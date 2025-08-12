namespace Zoo;

public class Animal
{
	private string name;

	public string Name
	{
		get { return name; }
		set { name = value; }
	}

    public Animal(string name)
    {
        this.name = name;
    }


}