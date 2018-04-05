public class HouseController : BaseController
{
    /// <summary>
    ///     Fetch a house by name.
    /// </summary>
    public static House GetHouseByName(string name)
    {
        string query = "SELECT * FROM houses WHERE hName=@0";
        dynamic result = _database.Query(query, name);

        return new House(result);
    }

    /// <summary>
    ///     Fetch the house by id.
    /// </summary>
    public static House GetHouseById(int id)
    {
        string query = "SELECT * FROM houses WHERE hId=@0";
        dynamic result = _database.Query(query, id);

        return new House(result);
    }

    /// <summary>
    ///     Increse the amount of members by one
    /// </summary>
    public static void IncrementMemberCount(House house)
    {
        string query = $"UPDATE houses SET hMembers = hMembers + 1 WHERE hName=@0";
        _database.Execute(query, house.HouseName);
    }

    /// <summary>
    ///     Increase the amount of points.
    /// </summary>
    public static void IncrementMemberPoints(House house, int amount)
    {
        string query = "UPDATE houses SET hPoints = hPoints + @1 WHERE hName=@0";
        _database.Execute(query, house.HouseName, amount);
    }
}