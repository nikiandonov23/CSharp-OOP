using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using EDriveRent.Models.Contracts;
using EDriveRent.Repositories.Contracts;
using EDriveRent.Utilities.Messages;

namespace EDriveRent.Repositories;

public class UserRepository:IRepository<IUser>
{
    private List<IUser> users;

    public UserRepository()
    {
        users = new List<IUser>();
    }


    public void AddModel(IUser model)
    {
        users.Add(model);
    }
    public bool RemoveById(string identifier)
    {
        var toBeRemoved = users.FirstOrDefault(x => x.DrivingLicenseNumber == identifier);
        if (toBeRemoved!=null)
        {
            users.Remove(toBeRemoved);
            return true;
        }
        return false;

    }



    public IUser FindById(string identifier)
    {
        var toBeFinded = users.FirstOrDefault(x => x.DrivingLicenseNumber == identifier);
        if (toBeFinded != null)
        {
            
            return toBeFinded;
        }
        return null;
    }

    public IReadOnlyCollection<IUser> GetAll()
    {
        return users.AsReadOnly();
    }
}