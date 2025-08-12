namespace WildFarm.ANIMALS.BIRDS;

public class Hen:BaseBird
{
    public Hen(string name, double weight, double wingSize) : base(name, weight, wingSize)
    {
    }

    protected override double Increse => 0.35;
    public override string AskFood()
    {
        return "Cluck";
    }
}