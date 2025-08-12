namespace Raiding;

public class HealingHero:BaseHero
{
    public HealingHero(string name, int power) : base(name, power)
    {
    }

    public override string CastAbility()
    {
        return $"{this.GetType().Name} - {this.Name} healed for {this.Power}";
    }
}