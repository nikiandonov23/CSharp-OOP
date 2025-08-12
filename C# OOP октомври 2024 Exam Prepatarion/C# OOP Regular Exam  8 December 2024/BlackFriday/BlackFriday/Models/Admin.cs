namespace BlackFriday.Models;

public class Admin:User
{
    public Admin(string userName, string email) 
        : base(userName, email, true)
    {

    }
}