using WebMatrix.Data;

public class PortalController
{
    /// <summary>
    ///     Get the total member count of a house.
    /// </summary>
    /// <param name="houseId">The house in question</param>
    /// <returns>The total member count.</returns>
    public int GetHouseMemberCount(int houseId)
    {
        Database db = Database.Open("Database");
        string query = "SELECT uHouse FROM users WHERE uHouse=@0";
        dynamic result = db.Query(query, houseId);

        return result.Count;
    }

    /// <summary>
    ///     Get the total points of the house.
    /// </summary>
    /// <param name="houseId">House in question</param>
    /// <returns>The total points of the house.</returns>
    public int GetHousePoints(int houseId)
    {
        Database db = Database.Open("Database");
        string query = "SELECT hPoints FROM houses WHERE hId=@0";
        dynamic result = db.Query(query, houseId);
        
        if(result.Count > 0)
        {
            return result[0].hPoints;
        }
        else
        {
            return 0;
        }
    }

    /// <summary>
    ///     Gets the rank of the house
    /// </summary>
    /// <param name="houseId">The house in question</param>
    /// <returns>The rank of the house.</returns>
    public int GetHouseRank(int houseId)
    {
        Database db = Database.Open("Database");
        string query = "SELECT COUNT(*) AS rank FROM houses WHERE hPoints >= (SELECT hPoints FROM houses WHERE hId=@0)";

        dynamic result = db.Query(query, houseId);

        if (result.Count > 0)
        {
            return result[0].rank;
        }
        else
        {
            return 0;
        }
    }

    /// <summary>
    ///     Gets the points of the user.
    /// </summary>
    /// <param name="username">The user in question</param>
    /// <returns>The amount of points of the user.</returns>
    public int GetUserPoints(string username)
    {
        Database db = Database.Open("Database");
        string query = "SELECT uPoints FROM users WHERE uUname=@0";
        dynamic result = db.Query(query, username);

        if(result.Count > 0)
        {
            return result[0].uPoints;
        }
        else
        {
            return 0;
        }
    }

    /// <summary>
    ///     Determine the rank of the user.
    /// </summary>
    /// <param name="username">User in question</param>
    /// <returns>The rank of the user.</returns>
    public int GetUserRank(string username)
    {
        Database db = Database.Open("Database");
        string query = "SELECT COUNT(*) AS rank FROM users WHERE uPoints >= (SELECT uPoints FROM users WHERE uUname=@0)";

        dynamic result = db.Query(query, username);

        if(result.Count > 0)
        {
            return result[0].rank;
        }
        else
        {
            return 0;
        }
    }

    /// <summary>
    ///     Get the time table data from the database, join the lessons and time table together.
    /// </summary>
    /// <param name="lessonWeek">Which week we are wanting to take the lessons from.</param>
    /// <returns>dynamic with all the rows and stuff.</returns>
    public dynamic GetTimeTableData(int lessonWeek)
    {
        Database db = Database.Open("Database");
        string query = "SELECT lessons.name, lessons.teacher, lessons.color, timetable.day, timetable.time, timetable.duration " +
                       "FROM timetable " +
                       "INNER JOIN lessons ON timetable.lesson = lessons.Id " +
                       "WHERE timetable.lessonWeek = @0";

        return db.Query(query, lessonWeek);
    }
}