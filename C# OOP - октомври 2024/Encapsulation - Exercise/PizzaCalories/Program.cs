namespace PizzaCalories
{
    public class Program
    {
        public static void Main()
        {
            try
            {
                string[] pizzaData = Console.ReadLine().Split().ToArray();
                string pizzaName = pizzaData[1];
                Pizza pizza = new Pizza(pizzaName);


                string input = "";
                while ((input = Console.ReadLine()) != "END")
                {
                    string[] tockens = input.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToArray();

                    if (tockens[0] == "Dough")
                    {
                        string flourType = tockens[1];
                        string bakingTechnique = tockens[2];
                        double weightInGrams = double.Parse(tockens[3]);
                        Dough dough = new Dough(flourType, bakingTechnique, weightInGrams);

                        pizza.Dough = dough;

                    }
                    else if (tockens[0] == "Topping")
                    {
                        string toppingType = tockens[1];
                        double weightInGrams = double.Parse(tockens[2]);


                        Topping topping = new Topping(toppingType, weightInGrams);
                        pizza.AddIngridients(topping);
                    }

                   
                }

                Console.WriteLine($"{pizza.Name} - {pizza.Calories:f2} Calories.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
       




        }
    }
}
