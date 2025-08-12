namespace demo;

public class Worker
{
    private int salary;

    public int Salary
    {
        get { return salary; }

        set
        {
            if (value > 650)

            {
                salary = value;
            }
            else
            {
                Console.WriteLine("minimum salary is 650");
            }
        }
    }
    private string name = "Niki";

    public string Name
    {
        get { return name = "Niki"; }
        set { name = value; }
    }









}