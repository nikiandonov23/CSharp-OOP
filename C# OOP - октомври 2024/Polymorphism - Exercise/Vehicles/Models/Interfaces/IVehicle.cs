namespace Vehicles.Models.Interfaces;

public interface IVehicle
{
    public double FuelQuantity { get; }
    public double FuelConsumption { get; }


    public bool Drive(int distance);
    public bool Refuel(int liters);
}