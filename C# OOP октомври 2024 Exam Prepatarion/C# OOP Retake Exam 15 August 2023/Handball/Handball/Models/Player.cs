using Handball.Models.Contracts;
using Handball.Utilities.Messages;
using System;
using System.Text;

namespace Handball.Models;

public abstract class Player:IPlayer
{
    private string name;
    private double rating;
    private string team;

    public Player(string name, double rating)
    {
        this.Name = name;
        this.Rating = rating;
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
                throw new ArgumentException(ExceptionMessages.PlayerNameNull);
            }
            name = value;    
        }
    }
    public double Rating
    {
        get
        {
            return rating;
        }
        protected set       
        {
            if (value>10)
            {
                rating = 10;
                return;
            }
            else if (value<1)
            {
                rating = 1;
                return;
            }

            rating = value;
        }
    }
    public string Team
    {
        get { return team; }
    }



    public void JoinTeam(string name)
    {
        team=name;
    }


    public abstract void IncreaseRating();
    public abstract void DecreaseRating();


    public override string ToString()
    {
        StringBuilder result = new StringBuilder();
        result.AppendLine($"{this.GetType().Name}: {Name}");
        result.AppendLine($"--Rating: {Rating}");
        return result.ToString().Trim();
    }
}