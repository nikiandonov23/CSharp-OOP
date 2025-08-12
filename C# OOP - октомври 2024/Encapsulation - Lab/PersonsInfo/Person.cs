namespace PersonsInfo;

public class Person
{
    private string firstName;

    public string FirstName
    {
        get { return firstName; }
        set
        {
            if (value.Length < 3)
            {
                throw new ArgumentException("First name cannot contain fewer than 3 symbols!");
            }
            else
            {
                firstName = value;
            }


        }
    }


    private string lastName;

    public string LastName
    {
        get { return lastName; }
        set
        {
            if (value.Length < 3)
            {
                throw new ArgumentException("Last name cannot contain fewer than 3 symbols!");
            }

            lastName = value;
        }
    }


    private int age;

    public int Age
    {
        get { return age; }
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Age cannot be zero or a negative integer!");
            }
            age = value;
        }
    }



    private decimal salary;

    public decimal Salary
    {
        get { return salary; }
        set
        {
            if (value <= 650)
            {
                throw new ArgumentException("Salary cannot be less than 650 leva!");
            }

            salary = value;
        }
    }


    public void IncreaseSalary(decimal percentage)
    {
        if (Age < 30)
        {
            Salary += (Salary * percentage / 100) / 2;
        }
        else
        {
            Salary += Salary * percentage / 100;
        }
    }


    public Person(string firstName, string lastName, int age, decimal salary)
    {
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Age = age;
        this.Salary = salary;
    }

    public override string ToString()
    {
        return $"{this.FirstName} {this.LastName} receives {this.Salary:f2} leva.";
    }
}