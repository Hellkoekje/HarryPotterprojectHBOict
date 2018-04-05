/// <summary>
///     Object representation of the house table row.
/// </summary>
public struct House
{
    public int Id;
    public string HouseName;
    public int MembersCount;
    public int Points;

    /// <summary>
    ///     Construct the house object based on a query result.
    /// </summary>
    /// <param name="queryResult"></param>
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