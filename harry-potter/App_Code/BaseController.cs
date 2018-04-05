using WebMatrix.Data;

public class BaseController
{
    protected static Database database;

    /// <summary>
    ///     Get the current database context or open a new one.
    /// </summary>
    protected static Database _database
    {
        get
        {
            if (database == null ||
                database.Connection.State != System.Data.ConnectionState.Open)
            {
                database = Database.Open("Database");
            }

            return database;
        }
    }
}