namespace PizzaCalories;

public class Pizza
{
    private readonly List<Topping> _toppings;


    public string Name { get; }
    public Dough Dough { get; set; } = null!;
    public IReadOnlyCollection<Topping> Toppings { get; }
    public double Calories => this.Dough.Calories + this.Toppings.Sum(x => x.Calories);

    public Pizza(string name)
    {
        if (string.IsNullOrEmpty(name) || name.Length > 15) throw new ArgumentException("Pizza name should be between 1 and 15 symbols.");

     
        
        Name = name;
        
        _toppings= new List<Topping>();
        Toppings = _toppings.AsReadOnly();

    }

    public void AddIngridients(Topping topping)
    {
        if (_toppings.Count == 10) throw new ArgumentException("Number of toppings should be in range [0..10].");
       
        _toppings.Add(topping);
    }


}