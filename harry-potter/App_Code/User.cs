public struct User
{
    public readonly int Id;
    public readonly string Email;
    public readonly string Username;
    public readonly int House;
    public readonly int Points;

    public User(dynamic queryResult)
    {
        if (queryResult.Count > 0)
        {
            Id = queryResult[0].uId;
            Email = queryResult[0].uEmail;
            Username = queryResult[0].uUname;
            House = queryResult[0].uHouse;
            Points = queryResult[0].uPoints;
        }
        else
        {
            Id = -1;
            Email = "";
            Username = "";
            House = 0;
            Points = 0;
        }
    }
}