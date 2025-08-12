namespace BorderControl;

public class Citizen:IIdentifiable,IBirthable,INamable,IBuyer
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Id { get; set; }
    public string BirthDate { get; set; }
    public int Food { get; set; } = 0;

    public Citizen(string name, int age, string id,string birthDate)
    {
        Name = name;
        Age = age;
        Id = id;
        BirthDate= birthDate;
    }


    
    public void BuyFood()
    {
        this.Food += 10;
    }
}