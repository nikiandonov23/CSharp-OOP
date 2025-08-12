namespace Animals;

public class Dog:Animal
{
    public override string ExplainSelf()
    {
        return $"I am {this.Name} and my fovourite food is {this.FavouriteFood}\r\nDJAAF";
    }


    public Dog(string name, string favouriteFood) : base(name, favouriteFood)
    {

    }
}