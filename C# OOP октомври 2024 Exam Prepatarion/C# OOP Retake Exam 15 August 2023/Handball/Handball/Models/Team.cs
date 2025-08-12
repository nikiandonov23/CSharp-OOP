using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Handball.Models.Contracts;
using Handball.Utilities.Messages;

namespace Handball.Models;

public class Team:ITeam
{
    private string name;
    private int pointsEarned;
    private double overallRating;
    private List<IPlayer> players;

    public Team(string name)
    {
        this.name = name;
        players=new List<IPlayer>();
    }


    public string Name
    {
        get
        {
            return name;
        }
        private set
        {
            if (String.IsNullOrWhiteSpace(value))            
            {
                throw new ArgumentException(ExceptionMessages.TeamNameNull);
            }
            name = value;    
        }
    }
    public int PointsEarned
    {
        get
        {
            return pointsEarned;
        }
        private set
        {

            pointsEarned = value;    
        }
    }
    public double OverallRating
    {
        get
        {
            if (players.Count==0)
            {
                return 0;
            }

            return Math.Round(players.Average(p => p.Rating), 2);
        }
      
    }
    public IReadOnlyCollection<IPlayer> Players
    {
        get { return players.AsReadOnly(); }
    }

    public void SignContract(IPlayer player)
    {
        players.Add(player);
    }

    public void Win()
    {
        PointsEarned += 3;
        foreach (var pl in players)
        {
            pl.IncreaseRating();
        }
    }

    public void Lose()
    {
        foreach (var pl in players)
        {
            pl.DecreaseRating();
        }
    }

    public void Draw()
    {
        PointsEarned += 1;
        foreach (var pl in players)
        {
            if (pl.GetType().Name==nameof(Goalkeeper))
            {
                pl.IncreaseRating();
            }
        }
    }   //може да гръмне ...

    public override string ToString()
    {
        StringBuilder result = new StringBuilder();
        result.AppendLine($"Team: {Name} Points: {PointsEarned}");
        result.AppendLine($"--Overall rating: {OverallRating}");

        if (players.Count == 0)
        {
            result.AppendLine("--Players: none");
        }
        else
        {
            string playerNames = string.Join(", ", players.Select(p => p.Name));
            result.AppendLine($"--Players: {playerNames}");
        }

        return result.ToString().TrimEnd();
    }


}