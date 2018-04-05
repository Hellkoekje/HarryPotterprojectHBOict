
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
}