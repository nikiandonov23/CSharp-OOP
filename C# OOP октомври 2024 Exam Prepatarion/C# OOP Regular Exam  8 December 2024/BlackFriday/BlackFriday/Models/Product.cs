using BlackFriday.Models.Contracts;
using BlackFriday.Utilities.Messages;
using System.Xml.Linq;

namespace BlackFriday.Models;

public abstract class Product : IProduct
{
    private string productName;
    private double basePrice;
    private bool isSold;

    public Product(string productName, double basePrice)
    {
        this.ProductName = productName;
        this.BasePrice = basePrice;
        this.IsSold = false;
    }




    public string ProductName
    {
        get
        {
            return productName;
        }
        private set
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(ExceptionMessages.ProductNameRequired);
            }
            productName = value;
        }
    }
    public double BasePrice
    {
        get
        {
            return basePrice;
        }
        private set
        {
            if (value <= 0)
            {
                throw new ArgumentException(ExceptionMessages.ProductPriceConstraints);
            }
            basePrice = value;
        }
    }
    public abstract double BlackFridayPrice { get; }
    public bool IsSold
    {
        get { return isSold; }
        private set { isSold = value; }
    }


    public override string ToString()
    {
        return $"Product: {ProductName}, Price: {BasePrice:F2}, You Save: {(BasePrice - BlackFridayPrice):F2}";
    }


    public void UpdatePrice(double newPriceValue)
    {
        this.BasePrice=newPriceValue;
    }

    public void ToggleStatus()
    {
        this.IsSold = !IsSold;
    }
}