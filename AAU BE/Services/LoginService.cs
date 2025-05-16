using AAU_BE.Database;
using DefaultNamespace;

namespace AAU_BE.Models;
using System.Collections.Generic;
using System.Data;

public class LoginService
{
    private readonly string _connectionString;

    public bool AddUser(LoginRequest lR)
    {
        var bindparams = new Dictionary<string, dynamic>
        {
            { "username", lR.username },
            { "password", lR.password },
            {"email", lR.email },
            {"userlevel", "User"}
        };
        if(Database.SQLDB.DoSqlCommand(Queries.INSERTUSER, bindparams) > 0)
        {
            return true;
        }
        return false;
    }


    public class AuthResult
    {
        public bool IsAuthenticated { get; set; }
        public int UserId { get; set; }
        public string UserLevel { get; set; }
    }

    public AuthResult AuthenticateUser(string username, string password)
    {
        // Bind parameters to prepare statements.
        var bindparams = new Dictionary<string, dynamic>
        {
            { "username", username }
        };
Console.WriteLine(username);
        DataTable dT = SQLDB.FetchDataTable(Queries.RETRIEVEUSER, bindparams); // Retrieve user first
        if (dT != null && dT.Rows.Count > 0)
        {
            string storedPassword = dT.Rows[0]["Password"].ToString();
        
            if (VerifyPassword(password, storedPassword)) // Check sent password versus stored in Database
            {
                // Login successful
                return new AuthResult
                {
                    IsAuthenticated = true,
                    UserId = Convert.ToInt32(dT.Rows[0]["Id"]), 
                    UserLevel = dT.Rows[0]["UserLevel"].ToString() 
                };
            }
        }
    
        return new AuthResult { IsAuthenticated = false };
    }

    private bool VerifyPassword(string inputPassword, string storedPassword)
    {
        //eventually we will use hashing
        return inputPassword == storedPassword;
    }
}