using CarDealership.Models.Contracts;
using CarDealership.Repositories;
using CarDealership.Repositories.Contracts;

namespace CarDealership.Models;

public class Dealership:IDealership
{
    private IRepository<IVehicle> vehicles;
    private IRepository<ICustomer> customers;

    public Dealership()
    {
        vehicles = new VehicleRepository();
        customers = new CustomerRepository();
    }

    public IRepository<IVehicle> Vehicles
    {
        get { return vehicles; }
    }

    public IRepository<ICustomer> Customers
    {
        get { return customers; }
    }
}
