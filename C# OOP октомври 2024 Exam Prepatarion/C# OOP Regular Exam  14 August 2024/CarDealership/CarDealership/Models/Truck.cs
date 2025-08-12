namespace CarDealership.Models;

public class Truck:Vehicle
{
    public Truck(string model, double price) : base(model, price*1.30)
    {

    }
}