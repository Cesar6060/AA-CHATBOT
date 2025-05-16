namespace DefaultNamespace;

public class Queries
{
    public const string RETRIEVEUSER = "SELECT Id, Username, Password, UserLevel FROM Users WHERE Username = @username";
    public const string UPDATEUSERTOADMIN = "UPDATE Users SET UserLevel = \"Admin\" WHERE USERNAME = @username";
    public const string INSERTUSER = "INSERT INTO Users (Username, Password, UserLevel, Email, Active) VALUES (@username, @password, @userlevel, @email, 1)";
    public const string UPDATEAPPOINTMENT = "Update appointments set Description = @description, AppointmentDate = @appointmentdate, AppointmentTime = @appointmenttime, IsCompleted = @iscompleted  where id = @id";
    public const string INSERTFAQ = "INSERT INTO FAQ(Question, Answer) VALUES (@question, @answer)";
    public const string INSERTAPPOINTMENT = "INSERT INTO appointments (Description, AppointmentDate, AppointmentTime, UserId) VALUES (@description, @appointmentdate, @appointmenttime, @userid)";
    public const string INSERTPET =
        "INSERT INTO UserPets (userid, age, breed, name, species, sex) VALUES (@userid, @age, @breed, @name, @species, @sex)";
    public const string RETRIEVEAPPOINTMENT = "SELECT ID from Appointments WHERE userid = @userid and description = @description";
    public const string UPDATEPET = "UPDATE UserPets SET age = @age, breed = @breed, name = @name, species = @species, sex = @sex WHERE id = @id";
    public const string MARKUSERINACTIVE = "UPDATE Users SET Active = 1 Where Id = @id";
    public const string UPDATEPASSWORD = "UPDATE Users SET Password = @password Where Id = @id";
    public const string UPDATEFAQ = "UPDATE FAQ SET Question = @question, Answer = @answer WHERE Id = @id";
    public const string ALLUSERS = "SELECT * FROM Users";
    public const string APPOINTMENTBYUSER = "SELECT * FROM Appointments WHERE UserId = @id";
    public const string PETSBYUSER = "SELECT * FROM UserPets WHERE userid = @id";
    public const string DELETEAPPOINTMENTBYUSER = "DELETE FROM Appointments WHERE Id = @id";
    public const string DELETEPET = "DELETE FROM userpets where id = @id";
   // public const string UPDATEAPPOINTMENT = "UPDATE appointments SET Description = @Description, AppointmentDate = @AppointmentDate, AppointmentTime = @AppointmentTime, IsCompleted = @IsCompleted WHERE Id = @Id";
    public const string ALLFAQ = "SELECT * FROM FAQ";
    public const string GETFAQID = "SELECT ID from FAQ where question = @question";
    public const string DELETEUSER = "DELETE FROM Users WHERE Id = @id";
    public const string DELETEFAQ = "DELETE FROM FAQ WHERE Id = @id";
    public const string GETUSERNAME = "SELECT Username from Users where Id = @id";
    public const string GETUSERID = "SELECT Id from Users where Username = @username";
    public const string UPDATEUSER = "UPDATE Users SET Password = @password, UserLevel = @userlevel, Username = @username, Email = @email WHERE Id = @id";
}