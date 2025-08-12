namespace CarDealership.Models;

public class SaloonCar:Vehicle
{


    public SaloonCar(string model, double price) : base(model, price*1.10)
    {

    }
}