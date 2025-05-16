using System.Data;

namespace AAU_BE.Database;
using Microsoft.Data.Sqlite;

public class SQLDB
{
    //for mac users(we can make this an env variable later):
    public static string connectionString = "Data Source=./Database/AAU.DB";
    
    //for windows users:
    //public static string connectionString = "Data Source=|DataDirectory|\\Database\\AAU.DB";
    private static SqliteConnection conn;
    
    private static void GetConnection(string connectionString)
    {
        if (conn == null)
        {
            conn = new SqliteConnection(connectionString);
        }

        if (conn.State == ConnectionState.Closed)
        {
            conn.Open();
        }
    }

    // Bind parameters to SqliteCommand 
    public static void BindParameters(SqliteCommand cmd, Dictionary<string, dynamic> bindparams)
    {
        foreach (KeyValuePair<string, dynamic> param in bindparams)
        {
            // Parameters in SQLite use `@` prefix
            cmd.Parameters.AddWithValue("@" + param.Key, param.Value);
        }
    }
    
    // Method to execute Insert, Update, or Delete queries
    public static int DoSqlCommand(string query, Dictionary<string, dynamic> bindparams)
    {
        int rowsAffected = 0;

        using (var conn = new SqliteConnection(connectionString))
        {
            conn.Open();

            using (var cmd = new SqliteCommand(query, conn))
            {
                BindParameters(cmd, bindparams);
                try
                {
                    // Execute the query and return the number of rows affected
                    rowsAffected = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"SQL error: {ex.Message}");
                }
            }
        }
        return rowsAffected;
    }

    // Fetch DataTable using raw SQL and bound parameters
    public static DataTable FetchDataTable(string query, Dictionary<string, dynamic> bindparams)
    {
        GetConnection(connectionString);

        DataTable dT = new DataTable();

        try
        {
            using (var cmd = new SqliteCommand(query, conn))
            {
                // Bind parameters to the command
                BindParameters(cmd, bindparams);
                
                using (SqliteDataReader reader = cmd.ExecuteReader())
                {
                    dT.Load(reader);
                }
            }
        }
        catch (Exception ex)
        {
            dT = null;
        }

        return dT;
    }
    public static DataTable FetchDataTable(string query)
    {
        GetConnection(connectionString);

        DataTable dT = new DataTable();

        try
        {
            using (var cmd = new SqliteCommand(query, conn))
            {
                using (SqliteDataReader reader = cmd.ExecuteReader())
                {
                    dT.Load(reader); 
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            dT = null; 
        }

        return dT;
    }
}