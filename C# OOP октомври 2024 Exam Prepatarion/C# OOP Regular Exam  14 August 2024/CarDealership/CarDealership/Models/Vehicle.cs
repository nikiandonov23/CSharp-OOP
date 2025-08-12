using CarDealership.Models.Contracts;
using CarDealership.Utilities.Messages;

namespace CarDealership.Models;

public abstract class Vehicle:IVehicle
{
    private string model;
    private double price;
    private List<string> buyers;
    private int salesCount;


    public Vehicle(string model, double price)
    {
        this.Model = model;
        this.Price = price;
        this.buyers = new List<string>();
    }




    public string Model
    {
        get { return model; }
        private set
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(ExceptionMessages.ModelIsRequired);
            }

            model = value;
        }
    }
    public double Price
    {
        get { return price; }
        private set
        {
            if (value<=0)
            {
                throw new ArgumentException(ExceptionMessages.PriceMustBePositive);
            }

            price = value;
        }
    }
    public IReadOnlyCollection<string> Buyers
    {
        get { return buyers.AsReadOnly(); }
    }
    public int SalesCount
    {
         get { return salesCount; }
        private set
        {


            salesCount = value;
        }
       
    }  




    public void SellVehicle(string buyerName)
    {
       buyers.Add(buyerName);
       salesCount++;
    }


    public override string ToString()
    {
        return $"{Model} - Price: {Price:F2}, Total Model Sales: {SalesCount}";
    }
}