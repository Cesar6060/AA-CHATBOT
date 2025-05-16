using System;
using AAU_BE.Database;
using DefaultNamespace;

namespace AAU_BE.Models;
using System.Collections.Generic;
using System.Data;

public class UserService
{
    private readonly string _connectionString;

    public bool UpdateUserPassword(int userId, string password)
    {
        var bindparams = new Dictionary<string, dynamic>
        {
            { "password", password },
            { "id", userId },
        };
        if (Database.SQLDB.DoSqlCommand(Queries.UPDATEPASSWORD, bindparams) > 0)
        {
            return true;
        }

        return false;
    }

    public bool DeleteUser(int userId)
    {
        var bindparams = new Dictionary<string, dynamic>
        {
            { "id", userId },
        };
        if (Database.SQLDB.DoSqlCommand(Queries.DELETEUSER, bindparams) > 0)
        {
            return true;
        }
        return false;
    }
    
    public bool UpdateUser(User user)
    {
        var bindparams = new Dictionary<string, dynamic>
        {
            { "password", user.password },
            {"userlevel", user.userlevel},
            {"username", user.username},
            {"email", user.email},
            {"id", user.id },
        };
        if (Database.SQLDB.DoSqlCommand(Queries.UPDATEUSER, bindparams) > 0)
        {
            return true;
        }
        return false;
    }


    public bool MarkAccountInactive(int userId)
    {
        var bindparams = new Dictionary<string, dynamic>
        {
            { "id", userId },
        };
        if (Database.SQLDB.DoSqlCommand(Queries.MARKUSERINACTIVE, bindparams) > 0)
        {
            return true;
        }

        return false;
    }

    public List<User> GetAllUsers()
    {
        DataTable dT = SQLDB.FetchDataTable(Queries.ALLUSERS);
        List<User> users = new List<User>();

        foreach (DataRow row in dT.Rows)
        {
            User user = new User
            {
                id = Convert.ToInt32(row["id"]),
                username = row["Username"].ToString(),
                password = row["Password"].ToString(),
                email = row["Email"].ToString(),
                active = Convert.ToInt32(row["Active"]),
                userlevel = row["UserLevel"].ToString()
            };

            users.Add(user);
        }

        return users;
    }

    public string GetUserName(int id)
    {
        var bindparams = new Dictionary<string, dynamic>
        {
            { "id", id },
        };
        DataTable dT = Database.SQLDB.FetchDataTable(Queries.GETUSERNAME, bindparams);
        string userName = dT.Rows[0]["username"].ToString();
        return userName;
    }
    
    public int GetUserId(string username)
    {
        var bindparams = new Dictionary<string, dynamic>
        {
            { "username", username },
        };
        DataTable dT = Database.SQLDB.FetchDataTable(Queries.GETUSERID, bindparams);
        int userId = Convert.ToInt32(dT.Rows[0]["id"]);
        return userId;
    }
}