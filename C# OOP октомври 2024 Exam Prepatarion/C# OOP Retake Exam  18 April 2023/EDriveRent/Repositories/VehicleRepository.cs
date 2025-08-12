using System.Collections.Generic;
using System.Linq;
using EDriveRent.Models.Contracts;
using EDriveRent.Repositories.Contracts;

namespace EDriveRent.Repositories;

public class VehicleRepository:IRepository<IVehicle>
{
    private List<IVehicle> vehicles;

    public VehicleRepository()
    {
        vehicles = new List<IVehicle>();
    }


    public void AddModel(IVehicle model)
    {
        vehicles.Add(model);
    }

    public bool RemoveById(string identifier)
    {
        var vehicleToBeRemoved = vehicles.FirstOrDefault(x => x.LicensePlateNumber == identifier);
        if (vehicleToBeRemoved != null)
        {
            vehicles.Remove(vehicleToBeRemoved);
            return true;
        }

        return false;

    }

    public IVehicle FindById(string identifier)
    {
        var vehicleToBeFound = vehicles.FirstOrDefault(x => x.LicensePlateNumber == identifier);

        if (vehicleToBeFound != null)
        {
            
            return vehicleToBeFound;
        }

        return null;
    }

    public IReadOnlyCollection<IVehicle> GetAll()
    {
        return vehicles.AsReadOnly();
    }
}