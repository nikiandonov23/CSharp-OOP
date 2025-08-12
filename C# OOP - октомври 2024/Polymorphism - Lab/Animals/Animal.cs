namespace Animals;

public abstract class Animal
{
    public string Name { get; set; }
    public string 	FavouriteFood { get; set; }

   public  abstract string ExplainSelf();

   protected Animal(string name, string favouriteFood)
   {
       Name = name;
       FavouriteFood = favouriteFood;
   }
}