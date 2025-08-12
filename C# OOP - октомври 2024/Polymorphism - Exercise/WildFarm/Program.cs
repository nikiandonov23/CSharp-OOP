using WildFarm.ANIMALS;
using WildFarm.ANIMALS.BIRDS;
using WildFarm.ANIMALS.MAMMALS;
using WildFarm.ANIMALS.MAMMALS.FELINE;
using WildFarm.FOODS;

namespace WildFarm
{
    public class Program
    {
        static void Main()
        {
            List<Animal> allAnimals = new List<Animal>();
            int counter = 0;
            string input = "";
            while ((input = Console.ReadLine()) != "End")
            {



                string[] tockens = input.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToArray();
                string type = tockens[0];

                Animal animal = null;
                switch (type)
                {
                    case "Owl":
                        animal = new Owl(tockens[1], double.Parse(tockens[2]), double.Parse(tockens[3]));
                        break;

                    case "Hen":
                        animal = new Hen(tockens[1], double.Parse(tockens[2]), double.Parse(tockens[3]));
                        break;

                    case "Mouse":
                        animal = new Mouse(tockens[1], double.Parse(tockens[2]), tockens[3]);
                        break;

                    case "Cat":
                        animal = new Cat(tockens[1], double.Parse(tockens[2]), tockens[3], tockens[4]);
                        break;

                    case "Dog":
                        animal = new Dog(tockens[1], double.Parse(tockens[2]), tockens[3]);
                        break;

                    case "Tiger":
                        animal = new Tiger(tockens[1], double.Parse(tockens[2]), tockens[3], tockens[4]);
                        break;
                    default:
                        break;
                }
                

                input=Console.ReadLine();
                tockens = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                type = tockens[0];
                Food food = null;
                switch (type)
                {
                    case "Vegetable":
                        food = new Vegetable(int.Parse(tockens[1]));
                        break;

                    case "Fruit":
                        food = new Fruit(int.Parse(tockens[1]));
                        break;

                    case "Meat":
                        food = new Meat(int.Parse(tockens[1]));
                        break;

                    case "Seeds":
                        food = new Seeds(int.Parse(tockens[1]));
                        break;
                }
                Console.WriteLine(animal.AskFood());
                if (animal.Eat(food))
                {
                    
                }
                else
                {
                    Console.WriteLine($"{animal.GetType().Name} does not eat {food.GetType().Name}!");
                }
                allAnimals.Add(animal);
              


                

            }

            foreach (var animal in allAnimals)
            {
                Console.WriteLine(animal.ToString());
            }
        }
    }
}
