namespace Raiding;

public class HittingHero:BaseHero
{
    public override string CastAbility()
    {
        return $"{this.GetType().Name} - {this.Name} hit for {this.Power} damage";
    }

    public HittingHero(string name, int power) : base(name, power)
    {
    }
}