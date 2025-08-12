using System.Security.Cryptography.X509Certificates;
using CarDealership.Models.Contracts;
using CarDealership.Repositories.Contracts;

namespace CarDealership.Repositories;

public class VehicleRepository:IRepository<IVehicle>
{
    private List<IVehicle> models;


    public VehicleRepository()
    {
        models = new List<IVehicle>();
    }


    public IReadOnlyCollection<IVehicle> Models
    {
        get { return models.AsReadOnly(); }
    }

    public void Add(IVehicle model)
    {
        models.Add(model);
    }
    public bool Remove(string text)
    {
        var modelToBeRemoved = models.FirstOrDefault(x => x.Model == text);
        if (modelToBeRemoved!=null)
        {
            models.Remove(modelToBeRemoved);
            return true;
        }

        return false;
    }

    public bool Exists(string text)
    {
        var modelToBeReturned=models.FirstOrDefault(x => x.Model == text);
        if (modelToBeReturned!=null)
        {
            return true;
        }
        return false;
    }

    public IVehicle Get(string text)
    {
        return models.FirstOrDefault(x => x.Model == text);
    }
}