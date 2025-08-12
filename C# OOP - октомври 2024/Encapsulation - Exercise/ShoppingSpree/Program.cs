using System.Runtime.CompilerServices;

namespace ShoppingSpree
{
    public class Program
    {
        public static void Main()
        {
            try
            {
                List<Person> people = ReadPeople();
                List<Product> products = ReadProducts();

                ProcessCommands(people, products);
                PrintOutput(people);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }


        private static void ProcessCommands(List<Person> people, List<Product> products)
        {
            Dictionary<string, Person> peopleByName = people.ToDictionary(x => x.Name);
            Dictionary<string, Product> productsByName = products.ToDictionary(x => x.Name);

            string input = "";
            while ((input = Console.ReadLine()) != "END")
            {
                string[] tockens = input.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToArray();
                string personName = tockens[0];
                string productName = tockens[1];

                Person person = peopleByName[personName];
                Product product = productsByName[productName];



                if (person.PurchaseProduct(product))
                {
                    Console.WriteLine($"{person.Name} bought {product.Name}");
                }
                else
                {
                    Console.WriteLine($"{person.Name} can't afford {product.Name}");
                }


            }


        }

        private static void PrintOutput(List<Person> people)
        {
            foreach (var person in people)
            {
                if (person.Bag.Count==0)
                {
                    Console.WriteLine($"{person.Name} - Nothing bought ");
                }
                else
                {
                    Console.WriteLine($"{person.Name} - {string.Join(", ",person.Bag.Select(x=>x.Name))}");  
                }

            }
        }














        private static List<Person> ReadPeople()
        {
            string[] input = Console.ReadLine().Split(";", StringSplitOptions.RemoveEmptyEntries);
            List<Person> allPeople = new List<Person>();
            foreach (var element in input)
            {
                string[] tockens = element.Split("=", StringSplitOptions.RemoveEmptyEntries).ToArray();
                string name = tockens[0];
                decimal money = decimal.Parse(tockens[1]);
                Person currentPerson = new Person(name, money);
                allPeople.Add(currentPerson);
            }
            return allPeople;
        }

        private static List<Product> ReadProducts()
        {
            List<Product> allProducts = new List<Product>();
            string[] input = Console.ReadLine().Split(";", StringSplitOptions.RemoveEmptyEntries).ToArray();
            foreach (var element in input)
            {
                string[] tockens = element.Split("=", StringSplitOptions.RemoveEmptyEntries).ToArray();
                Product currentProduct = new Product(tockens[0], decimal.Parse(tockens[1]));
                allProducts.Add(currentProduct);
            }
            return allProducts;
        }
    }
}
