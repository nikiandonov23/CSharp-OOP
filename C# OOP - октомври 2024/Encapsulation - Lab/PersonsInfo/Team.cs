using System.Text;

namespace PersonsInfo;

public class Team
{
    private string name;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }



    private List<Person> firstTeam;

    public List<Person> FirstTeam
    {
        get { return firstTeam; }

    }

    private List<Person> reserveTeam;

    public List<Person> ReserveTeam
    {
        get { return reserveTeam; }

    }


    public Team(string name)
    {
        this.name = name;
        this.firstTeam = new List<Person>();
        this.reserveTeam = new List<Person>();
    }

    public void AddPlayer(Person person)
    {
        if (person.Age < 30)
        {
            firstTeam.Add(person);
        }
        else
        {
            reserveTeam.Add(person);
        }
    }


    public override string ToString()
    {
        StringBuilder result = new StringBuilder();
        result.AppendLine($"First team has {FirstTeam.Count} players.");
        result.AppendLine($"Reserve team has {ReserveTeam.Count} players.");


        return result.ToString().Trim();
    }
}