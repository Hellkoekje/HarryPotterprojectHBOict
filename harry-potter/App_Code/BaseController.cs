using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebMatrix.Data;

/// <summary>
/// Summary description for BaseController
/// </summary>
public class BaseController
{
    protected static Database database;
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