using System;
using System.Text;
using System.Web.Helpers;
using WebMatrix.Data;

public enum RegisterState
{
    Ok,
    Failed,
    AlreadyExists
}

public enum LoginState
{
    Ok,
    Failed,
    Incorrect,

}

public class AuthController
{
    private static Database database;
    private static Database _database
    {
        get
        {
            if(database == null ||
                database.Connection.State != System.Data.ConnectionState.Open)
            {
                database = Database.Open("Database");
            }

            return database;
        }
    }

    private static int _loggedInId = -1;
    public static int LoggedInId
    {
        get
        {
            return _loggedInId;
        }
    }

    /// <summary>
    ///     Try to register a new user in the database.
    /// </summary>
    /// <returns>Registration state.</returns>
    public static RegisterState Register(string username, string password, string email)
    {
        //Encrypt the entered password
        string encryptedPassword = Encrypt(password);

        try
        {
            //Check if the username already exists.
            string checkDupeQuery = "SELECT uId FROM users WHERE uUname=@0 OR uEmail=@1";
            dynamic dupeCheckResult = _database.Query(checkDupeQuery, username, email);

            if(dupeCheckResult.Count >= 1)
            {
                return RegisterState.AlreadyExists;
            }

            //Add account to the database.
            string query = "INSERT INTO users (uUname, uPassword, uEmail, uHouse, uYear) VALUES (@0, @1, @2, @3, @4)";
            _database.Execute(query, username, encryptedPassword, email, 0, DateTime.Now.ToShortDateString());
            return RegisterState.Ok;
        }
        catch(Exception e)
        {
            return RegisterState.Failed;
        }
        finally
        {
            _database.Close();
        }
    }

    /// <summary>
    ///     Log in an user.
    /// </summary>
    /// <returns>State of the login.</returns>
    public static LoginState Login(string username, string password)
    {
        try
        {
            string query = "SELECT uId, uPassword FROM users WHERE uUname=@0";
            dynamic result = _database.Query(query, username);

            if (result.Count <= 0) return LoginState.Incorrect;

            bool valid = CheckPassword(password, result[0].uPassword);

            if (valid)
            {
                SetAuthedUser(result[0].uId);
                return LoginState.Ok;
            }

            return LoginState.Incorrect;
        }
        catch (Exception e)
        {
            return LoginState.Failed;
        }
        finally
        {
            _database.Close();
        }
    }

    /// <summary>
    ///     Tell the client we have a logged in user, represented by an id.
    /// </summary>
    /// <param name="id">Id of the user.</param>
    public static void SetAuthedUser(int id)
    {
        if (id == -1) return;

        _loggedInId = id;
    }

    private static string Encrypt(string password)
    {
        //TODO: Potentially salt the password.
        string hashed = Crypto.Hash(password, "SHA256");
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(hashed));
    }

    private static bool CheckPassword(string passwordInput, string passwordFromDb)
    {
        string passwordEncryptedInput = Encrypt(passwordInput);
        return passwordEncryptedInput.Equals(passwordFromDb, StringComparison.InvariantCulture);
    }
}