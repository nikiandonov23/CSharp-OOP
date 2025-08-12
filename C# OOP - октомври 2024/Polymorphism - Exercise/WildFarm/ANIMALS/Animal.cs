using System.ComponentModel.DataAnnotations;
using WildFarm.FOODS;

namespace WildFarm.ANIMALS;

public abstract class Animal
{
    public string Name { get;  }
    public double Weight { get;private set; }
    public int FoodEaten { get;private set; }

    protected abstract double Increse { get; }

    protected Animal(string name, double weight)
    {
        Name = name;
        Weight = weight;
        
    }

    public virtual bool Eat(Food food)
    {
        this.FoodEaten += food.Quantity;
        this.Weight += this.Increse * food.Quantity;
        return true;
    }

    public abstract string AskFood();
}