using WildFarm.FOODS;

namespace WildFarm.ANIMALS.MAMMALS;

public class Dog:BaseMammal
{
    public Dog(string name, double weight, string livingRegion) : base(name, weight, livingRegion)
    {
    }

    protected override double Increse => 0.40;
    public override string AskFood()
    {
        return "Woof!";
    }

    public override string ToString()
    {
        return $"{this.GetType().Name} [{this.Name}, {this.Weight}, {this.LivingRegion}, {this.FoodEaten}]";

    }

    public override bool Eat(Food food)
    {
        if (food.GetType().Name == "Meat")
        {
            return base.Eat(food);
        }

        return false;
    }
}