using WildFarm.FOODS;

namespace WildFarm.ANIMALS.MAMMALS;

public class Mouse:BaseMammal
{
    public Mouse(string name, double weight, string livingRegion) : base(name, weight, livingRegion)
    {
    }

    protected override double Increse => 0.10;
    public override string AskFood()
    {
        return "Squeak";
    }

    public override string ToString()
    {
        return $"{this.GetType().Name} [{this.Name}, {this.Weight}, {this.LivingRegion}, {this.FoodEaten}]";
    }

    public override bool Eat(Food food)
    {
        if (food.GetType().Name == "Vegetable" || food.GetType().Name == "Fruit")
        {
            return base.Eat(food);
        }

        return false;


    }
}