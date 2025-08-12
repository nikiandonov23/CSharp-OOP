namespace PlayersAndMonsters;

public class Hero
{
	private string username;

	public string Username
	{
		get { return username; }
		set { username = value; }
	}
	private int level;

	public int Level
	{
		get { return level; }
		set { level = value; }
	}



    public Hero(string username, int level)
    {
        this.username = username;
        this.level = level;
    }

    public override string ToString()
    {
        return $"Type: {this.GetType().Name} Username: {this.Username} Level: {this.Level}";
    }
}