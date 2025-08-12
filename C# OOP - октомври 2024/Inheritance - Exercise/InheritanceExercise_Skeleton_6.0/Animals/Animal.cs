using System;
using System.Security.Principal;
using System.Text;

namespace Animals;

public abstract class Animal
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Gender { get; set; }

    public Animal(string name, int age, string gender)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentNullException("Invalid input!");

        }
        if (age < 0)
        {
            throw new ArgumentException("Kura mi qnko");
        }

        if (string.IsNullOrEmpty(gender))
        {
            throw new ArgumentException("Invalid input!");
        }

        Name = name;
        Age = age;
        Gender = gender;
    }

    public virtual string ProduceSound()
    {
        return string.Empty.Trim();
    }


    public override string ToString()
    {
        StringBuilder result = new StringBuilder();
        result.AppendLine($"{this.GetType().Name}");
        result.AppendLine($"{this.Name} {this.Age} {this.Gender}");
        result.AppendLine(ProduceSound());
        return result.ToString().Trim();
    }
}