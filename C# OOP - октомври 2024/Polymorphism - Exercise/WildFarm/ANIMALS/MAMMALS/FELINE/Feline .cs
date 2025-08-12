namespace WildFarm.ANIMALS.MAMMALS.FELINE;

public abstract class Feline : BaseMammal
{

    public string Breed { get; set; }

    protected Feline(string name, double weight, string livingRegion, string breed) : base(name, weight, livingRegion)
    {
        Breed = breed;
    }

    protected override double Increse { get; }
    public override string ToString()
    {
        return
            $"{this.GetType().Name} [{this.Name}, {this.Breed}, {this.Weight}, {this.LivingRegion}, {this.FoodEaten}]";
    }
}