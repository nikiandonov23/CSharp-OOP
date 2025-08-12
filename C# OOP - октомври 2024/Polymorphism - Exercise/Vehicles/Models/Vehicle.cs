using Vehicles.Models.Interfaces;

namespace Vehicles.Models;

public abstract class Vehicle:IVehicle
{
    public double FuelQuantity { get; private set; }
    public virtual double FuelConsumption { get; private set; }
    public bool Drive(int distance)
    {
        if (FuelQuantity<distance*FuelConsumption)
        {
            return false;
        }

        FuelQuantity-=distance*FuelConsumption;
        return true;
    }

    public bool Refuel(int liters)
    {
        FuelQuantity += liters;
        return true;
    }

    protected Vehicle(double fuelQuantity, double fuelConsumption)
    {
        FuelQuantity = fuelQuantity;
        FuelConsumption = fuelConsumption;
    }

    public override string ToString()
    {
        return $"{this.GetType().Name}: {FuelQuantity:f2}";
    }
}