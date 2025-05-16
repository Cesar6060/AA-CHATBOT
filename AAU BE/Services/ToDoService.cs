using AAU_BE.Database;
using DefaultNamespace;

namespace AAU_BE.Models;
using System.Collections.Generic;
using System.Data;

public class ToDoService
{
    private readonly string _connectionString;

     public List<Appointment> GetAllTodos(int userId)
    {
        var bindparams = new Dictionary<string, dynamic>
        {
            { "id", userId },
        };
        var todos = new List<Appointment>();
        DataTable dT = SQLDB.FetchDataTable(Queries.APPOINTMENTBYUSER, bindparams);
        foreach (DataRow row in dT.Rows)
        {
            Appointment apps = new Appointment()
            {
                Id = Convert.ToInt32(row["Id"]),
                Description = row["Description"].ToString(),
                AppointmentDate = Convert.ToDateTime(row["AppointmentDate"]),  
                AppointmentTime = Convert.ToDateTime(row["AppointmentTime"]),  
                IsCompleted = Convert.ToBoolean(row["IsCompleted"]),
            };
            todos.Add(apps);
        }
        return todos;
    }

    public bool CreateTodo(Appointment appointment)
    {
        var bindparams = new Dictionary<string, dynamic>
        {
            {"userid", appointment.UserId},
            { "description", appointment.Description },
            { "appointmentdate", appointment.AppointmentDate },
            { "appointmenttime", appointment.AppointmentTime },
            { "iscompleted", appointment.IsCompleted },
        };
        if(Database.SQLDB.DoSqlCommand(Queries.INSERTAPPOINTMENT, bindparams) > 0)
        {
            return true;
        }
        return false;
    }

    public int getTodoID(Appointment appointment)
    {
        var bindparams = new Dictionary<string, dynamic>
        {
            {"userid", appointment.UserId},
            { "description", appointment.Description },
        };
        DataTable dT = SQLDB.FetchDataTable(Queries.RETRIEVEAPPOINTMENT, bindparams);
        return Convert.ToInt32(dT.Rows[0]["Id"]);
    }

    public bool UpdateTodo(int id, Appointment appointment)
    {
        var bindparams = new Dictionary<string, dynamic>
        {
            {"id", id},
            {"userid", appointment.UserId},
            { "description", appointment.Description },
            { "appointmentdate", appointment.AppointmentDate },
            { "appointmenttime", appointment.AppointmentTime },
            { "iscompleted", appointment.IsCompleted },
        };
        if (Database.SQLDB.DoSqlCommand(Queries.UPDATEAPPOINTMENT, bindparams) > 0)
        {
            return true;
        }
        return false;
    }
    
    public bool DeleteToDo(int id)
    {
        var bindparams = new Dictionary<string, dynamic>
        {
            { "id", id },
        };
        if (Database.SQLDB.DoSqlCommand(Queries.DELETEAPPOINTMENTBYUSER, bindparams) > 0)
        {
            return true;
        }
        return false;
    }
}