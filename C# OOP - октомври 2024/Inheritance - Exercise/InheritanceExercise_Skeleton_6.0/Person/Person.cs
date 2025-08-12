using System;
using System.Text;

namespace Person;

public class Person
{
    private string name;
    private int age;



    public Person()
    {
        
    }

    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }




    public int Age
    {
        get { return age; }
        set { age = value; }
    }
    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public override string ToString()
    {
        StringBuilder result = new StringBuilder();
        result.AppendLine($"{this.GetType().Name} -> Name: {this.Name}, Age: {this.Age}");
        return result.ToString().Trim();
    }
}