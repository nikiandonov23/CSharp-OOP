using System.ComponentModel;

namespace PizzaCalories;

public class Dough
{

    private readonly Dictionary<string, double> _flourTypeModifiers = new Dictionary<string, double>(StringComparer.InvariantCultureIgnoreCase)
    {
        ["White"] = 1.5,
        ["Wholegrain"] = 1.0,
    };

    private readonly Dictionary<string, double> _bakingTechniqueModifiers = new Dictionary<string, double>(StringComparer.InvariantCultureIgnoreCase)
    {
        ["Crispy"] = 0.9,
        ["Chewy"] = 1.1,
        ["Homemade"] = 1.0
    };

    public Dough(string flourType, string bakingTechnique, double weightInGrams)
    {
        if (!_flourTypeModifiers.ContainsKey(flourType) || (!_bakingTechniqueModifiers.ContainsKey(bakingTechnique)))
            throw new ArgumentException("Invalid type of dough.");

        if (weightInGrams < 1 || weightInGrams > 200)
            throw new ArgumentException("Dough weight should be in the range [1..200].");

        FlourType = flourType;
        BakingTechnique = bakingTechnique;
        WeightInGrams = weightInGrams;
    }

    public string FlourType { get; }
    public string BakingTechnique { get; }
    public double WeightInGrams { get; }
    public double Calories =>
        2 * this.WeightInGrams * _flourTypeModifiers[this.FlourType] * _bakingTechniqueModifiers[this.BakingTechnique];




}