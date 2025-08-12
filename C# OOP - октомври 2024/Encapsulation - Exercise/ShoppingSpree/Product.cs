namespace ShoppingSpree;

public class Product
{
    public string Name { get; }
    public decimal Cost { get;  }

    public Product(string name, decimal cost)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be empty");
        if (cost < 0) throw new ArgumentException("Money cannot be negative");
        Name = name;
        Cost = cost;
    }
}