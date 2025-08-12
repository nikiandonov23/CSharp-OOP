using WildFarm.FOODS;

namespace WildFarm.ANIMALS.BIRDS;

public class Owl:BaseBird
{
    public Owl(string name, double weight, double wingSize) : base(name, weight, wingSize)
    {
    }

    protected override double Increse => 0.25;

    public override string AskFood()
    {
        return "Hoot Hoot";
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