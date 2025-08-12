namespace CarDealership.Models;

public class SUV:Vehicle
{
    public SUV(string model, double price) : base(model, price*1.20)
    {

    }
}