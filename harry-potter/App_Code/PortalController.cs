using WebMatrix.Data;

public class PortalController
{
    public int GetHouseMemberCount(int houseId)
    {
        Database db = Database.Open("Database");
        string query = "SELECT uHouse FROM users WHERE uHouse=@0";
        dynamic result = db.Query(query, houseId);

        return result.Count;
    }

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

    public dynamic GetTimeTableData(int lessonWeek)
    {
        Database db = Database.Open("Database");
        string query = "SELECT lesson, day, time, duration FROM timetable WHERE lessonWeek=@0";

        return db.Query(query, lessonWeek);
    }
}