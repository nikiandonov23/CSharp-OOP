using FootballManager.Models.Contracts;
using FootballManager.Utilities.Messages;
using System.Xml.Linq;

namespace FootballManager.Models;

public class Team : ITeam
{
    private string name;
    private int championshipPoints;
    private IManager teamManager;
    private int presentCondition;
    public Team(string name)
    {
        this.Name = name;
    }



    public string Name
    {
        get
        {
            return name;
        }
        private set
        {
            if (String.IsNullOrWhiteSpace(value))            // proverqvam value dali e null zashtoto tova sa mi  vhodnite danni .
            {
                throw new ArgumentException(ExceptionMessages.TeamNameNull);
            }
            name = value;    //name sa mi izhodnite danni
        }
    }

    public int ChampionshipPoints
    {
        get
        {
            return championshipPoints;
        }
        private set
        {

            championshipPoints = value;    //championshipPoints sa mi izhodnite danni
        }
    }

    public IManager TeamManager
    {
        get
        {
            return teamManager;
        }
        private set
        {

            teamManager = value;    //championshipPoints sa mi izhodnite danni
        }
    }


    public int PresentCondition
    {
        get
        {
            if (TeamManager==null)
            {
                return 0;
            }

            if (ChampionshipPoints==0)
            {
                return (int)(TeamManager.Ranking);
            }

            int condition = (int)(ChampionshipPoints * TeamManager.Ranking);
            return condition;

        }
     
    }

    public void SignWith(IManager manager)
    {
        TeamManager=manager;
    }

    public void GainPoints(int points)
    {
        ChampionshipPoints += points;
    }

    public void ResetPoints()
    {
        ChampionshipPoints = 0;
    }

    public override string ToString()
    {
        return $"Team: {Name} Points: {ChampionshipPoints:f2}";
    }

  
}