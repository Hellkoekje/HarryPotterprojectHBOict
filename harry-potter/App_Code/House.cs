public struct House
{
    public int Id;
    public string HouseName;
    public int MembersCount;
    public int Points;

    public House(dynamic queryResult)
    {
        if (queryResult.Count > 0)
        {
            Id = queryResult[0].hId;
            HouseName = queryResult[0].hName;
            MembersCount = queryResult[0].hMembers;
            Points = queryResult[0].hPoints;
        }
        else
        {
            Id = -1;
            HouseName = "";
            MembersCount = 0;
            Points = 0;
        }
    }

}