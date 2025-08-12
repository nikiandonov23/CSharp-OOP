using WildFarm.FOODS;

namespace WildFarm.ANIMALS.MAMMALS.FELINE;

public class Tiger : Feline
{
    public Tiger(string name, double weight, string livingRegion, string breed) : base(name, weight, livingRegion, breed)
    {
    }

    public override string AskFood()
    {
        return "ROAR!!!";
    }

    protected override double Increse => 1.00;
    public override bool Eat(Food food)
    {
        if (food.GetType().Name == "Meat")
        {
            return base.Eat(food);
        }

        return false;
    }
}