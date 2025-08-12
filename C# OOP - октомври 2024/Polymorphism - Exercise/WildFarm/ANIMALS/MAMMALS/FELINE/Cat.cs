using WildFarm.FOODS;

namespace WildFarm.ANIMALS.MAMMALS.FELINE;

public class Cat : Feline
{
    public Cat(string name, double weight, string livingRegion, string breed) : base(name, weight, livingRegion, breed)
    {
    }

    protected override double Increse => 0.30;

    public override string AskFood()
    {
        return "Meow";
    }

    public override bool Eat(Food food)
    {
        if (food.GetType().Name == "Vegetable" || food.GetType().Name == "Meat")
        {
            return base.Eat(food);
        }

        return false;
    }
}