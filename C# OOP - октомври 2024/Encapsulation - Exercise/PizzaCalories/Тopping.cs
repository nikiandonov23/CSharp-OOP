namespace PizzaCalories;

public class Topping
{
    public Topping(string toppingType, double weightInGrams)
    {

        if (!_toppingTypeModifiers.ContainsKey(toppingType))
            throw new ArgumentException($"Cannot place {toppingType} on top of your pizza.");
        if (weightInGrams < 1 || weightInGrams > 50)
            throw new ArgumentException($"{toppingType} weight should be in the range [1..50].");
       
        ToppingType = toppingType;
        WeightInGrams = weightInGrams;
    }

    private static Dictionary<string, double> _toppingTypeModifiers = new Dictionary<string, double>(StringComparer.InvariantCultureIgnoreCase)
    {
        ["meat"] = 1.2,
        ["veggies"] = 0.8,
        ["cheese"] = 1.1,
        ["sauce"] = 0.9

    };

    public string ToppingType { get; }
    public double WeightInGrams { get; }

    public double Calories => 2 * WeightInGrams * _toppingTypeModifiers[ToppingType];




}