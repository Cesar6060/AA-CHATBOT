namespace AAU.Services;

public class AdminService
{
        public string UserLevel { get; private set; } = "User"; 
        public bool IsAdmin => UserLevel == "Admin";
        
        public void SetUserLevel(string userLevel)
        {
            UserLevel = userLevel;
        }
    }
    