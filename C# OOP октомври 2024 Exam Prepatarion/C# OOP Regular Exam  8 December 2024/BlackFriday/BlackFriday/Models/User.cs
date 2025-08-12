using BlackFriday.Models.Contracts;
using BlackFriday.Utilities.Messages;
using System.Xml.Linq;

namespace BlackFriday.Models;

public abstract class User:IUser
{
    private string userName;
    private string email;
    private bool hasDataAccess;

    public User(string userName, string email, bool hasDataAccess)
    {
        this.UserName = userName;
        this.Email = email;
        this.HasDataAccess = hasDataAccess;
    }





    public string UserName
    {
        get
        {
            return userName;
        }
        private set
        {
            if (String.IsNullOrWhiteSpace(value))            
            {
                throw new ArgumentException(ExceptionMessages.UserNameRequired);
            }
            userName = value;    
        }
    }
    public bool HasDataAccess
    {
        get
        {
            return hasDataAccess;
        }
        private set
        {
            hasDataAccess = value;
        }

    } 
    public string Email
    {
        get
        {
            if (HasDataAccess)
            {
                return "hidden";
            }
            return email;
        }
        private set
        {
            if (!HasDataAccess) // Валидираме само ако HasDataAccess е false
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.EmailRequired);
                }
            }
            email = value; // Задаваме стойността без презаписване
        }
    }    //не съм сигурен дали е правилен беееее.. :((((


    public override string ToString()
    {
        return $"{UserName} - Status: {this.GetType().Name}, Contact Info: {Email}";
    }
}