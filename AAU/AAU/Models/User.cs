namespace AAU.Models;
public class User
{
    public int id { get; set; }
    public string username { get; set; }
    public string password { get; set; }
    public string email { get; set; }

    public int active { get; set; }
    public string userlevel { get; set; }
}
