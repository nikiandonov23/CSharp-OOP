using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;

namespace Animals
{
    public class StartUp
    {
        public static void Main(string[] args)
        {


            string animalType = "";
            while ((animalType = Console.ReadLine()) != "Beast!")
            {
                string[] data = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).ToArray();
                string name = data[0];
                int age = int.Parse(data[1]);
                string gender = data[2];

                Animal currentAnimal = null;
                bool isValid = false;

                try
                {

                    if (animalType == "Dog")
                    {
                        currentAnimal = new Dog(name, age, gender);

                    }

                    else if (animalType == "Frog")
                    {
                        currentAnimal = new Frog(name, age, gender);

                    }

                    else if (animalType == "Cat")
                    {
                        currentAnimal = new Cat(name, age, gender);

                    }

                    else if (animalType == "Kitten")
                    {
                        currentAnimal = new Kitten(name, age);

                    }

                    else if (animalType == "Tomcat")
                    {
                        currentAnimal = new Tomcat(name, age);

                    }

                    
                }

                catch (ArgumentException e)
                {

                    Console.WriteLine(e.Message);
                }

                if (currentAnimal!=null)
                {
                    Console.WriteLine(currentAnimal);
                }
              




            }




        }
    }
}
