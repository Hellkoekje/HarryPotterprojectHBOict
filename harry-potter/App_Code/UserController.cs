public class UserController : BaseController
{
    private static int lastId = -1;
    private static User lastUserData;

    /// <summary>
    ///     Fetch the user data by id.
    /// </summary>
    /// <param name="id">Id of the user.</param>
    /// <returns>User object we fetched.</returns>
    public static User GetUserById(int id)
    {
        if (lastId == id) return lastUserData;

        string query = "SELECT uId, uEmail, uUname, uHouse, uPoints FROM users WHERE uId=@0";
        dynamic result = _database.Query(query, id);

        lastUserData = new User(result);
        lastId = id;

        return lastUserData;
    }

    /// <summary>
    ///     Grab the house of the player.
    /// </summary>
    public static House GetHouse(int userId)
    {
        User user = GetUserById(userId);

        return HouseController.GetHouseById(user.House);
    }

    /// <summary>
    ///     Assign a house to a user.
    /// </summary>
    /// <param name="user">User we want to assign the house to.</param>
    /// <param name="house">The house we want to assign to this user.</param>
    public static void AssignHouse(User user, House house)
    {
        string query = "UPDATE users SET uHouse=@0 WHERE uUname=@1";
        _database.Execute(query, house.Id, user.Username);
        Invalidate();
    }

    /// <summary>
    ///     Increase the amount of points.
    /// </summary>
    public static void IncrementUserPoints(User user, int amount)
    {
        string query = "UPDATE users SET uPoints = uPoints + @1 WHERE uUname=@0";
        _database.Execute(query, user.Username, amount);

        Invalidate();
    }

    /// <summary>
    ///     Invalidate any cached user data.
    ///     Usually called when altering existing user data.
    /// </summary>
    public static void Invalidate()
    {
        lastId = -1;
        lastUserData = default(User);
    }
}