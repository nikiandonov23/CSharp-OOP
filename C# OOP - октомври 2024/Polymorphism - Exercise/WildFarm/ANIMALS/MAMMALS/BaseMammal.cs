namespace WildFarm.ANIMALS.MAMMALS;

public abstract class BaseMammal:Animal
{
    protected BaseMammal(string name, double weight, string livingRegion) : base(name, weight)
    {
        LivingRegion = livingRegion;
    }

    public string LivingRegion { get; set; }

}