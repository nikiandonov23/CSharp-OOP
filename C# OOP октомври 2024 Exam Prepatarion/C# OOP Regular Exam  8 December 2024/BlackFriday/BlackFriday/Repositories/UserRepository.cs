using BlackFriday.Models.Contracts;
using BlackFriday.Repositories.Contracts;

namespace BlackFriday.Repositories;

public class UserRepository:IRepository<IUser>
{
    private List<IUser> models;

    public UserRepository()
    {
        models = new List<IUser>();
    }


    public IReadOnlyCollection<IUser> Models
    {
        get { return models.AsReadOnly(); }
    }

    public void AddNew(IUser model)
    {
        models.Add(model);
    }

    public IUser GetByName(string name)
    {
        return models.FirstOrDefault(x => x.UserName == name);
    }

    public bool Exists(string name)
    {
        var userToBeChecked=models.FirstOrDefault(x => x.UserName == name);

        if (userToBeChecked!=null)
        {
            return true;
        }
        return false;
    }
}