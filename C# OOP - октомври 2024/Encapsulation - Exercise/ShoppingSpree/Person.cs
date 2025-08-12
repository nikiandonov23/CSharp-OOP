using System.Collections.ObjectModel;
using System.Net.Http.Headers;

namespace ShoppingSpree;

public class Person
{
    private readonly List<Product> _bag;


    public string Name { get;  }
    public decimal Money { get; private set; }   
    public ReadOnlyCollection<Product> Bag { get; }


    public Person(string name, decimal money)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be empty");
        if (money < 0) throw new ArgumentException("Money cannot be negative");


        Name = name;
        Money = money;
        _bag=new List<Product>();
        Bag = _bag.AsReadOnly();

    }

    public bool PurchaseProduct(Product product)
    {
        if (product.Cost>this.Money) return false;

        this.Money-=product.Cost;
        this._bag.Add(product);
        return true;
       
    }
}