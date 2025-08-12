namespace BlackFriday.Models;

public class Service:Product
{
    public Service(string productName, double basePrice) 
        : base(productName, basePrice)
    {

    }

    public override double BlackFridayPrice => BasePrice * 0.80;
}