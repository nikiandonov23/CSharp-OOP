namespace Vehicles.Models;

public class Truck:Vehicle
{
    public Truck(double fuelQuantity, double fuelConsumption) 
        : base(fuelQuantity, fuelConsumption)
    {
    }

    public override double FuelConsumption => base.FuelConsumption+1.6;
}