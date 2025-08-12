namespace BlackFriday.Models;

public class Item:Product
{
    public Item(string productName, double basePrice) : base(productName, basePrice)
    {
    }

    public override double BlackFridayPrice => BasePrice*0.70;
}