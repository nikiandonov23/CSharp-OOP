namespace WildFarm.ANIMALS.BIRDS;

public abstract class BaseBird : Animal
{
    public double WingSize { get; }

    protected BaseBird(string name, double weight,double wingSize) : base(name, weight)
    {
        this.WingSize = wingSize;
    }

    public override string ToString()
    {
        return $"{this.GetType().Name} [{this.Name}, {this.WingSize}, {this.Weight}, {this.FoodEaten}]";
    }
}