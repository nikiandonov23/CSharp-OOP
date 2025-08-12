using FootballManager.Models.Contracts;
using FootballManager.Utilities.Messages;

namespace FootballManager.Models;

public abstract class Manager : IManager
{
    private string name;
    private double ranking;


    protected Manager(string name, double ranking)
    {
        this.Name = name;
        this.Ranking = ranking;
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
                throw new ArgumentException(ExceptionMessages.ManagerNameNull);
            }
            name = value;
        }
    }

    
    public double Ranking
    {
        get
        {
            return ranking;
        }
        protected set 
        {

            ranking = value;
        }
    }

    public abstract void RankingUpdate(double updateValue);

    public override string ToString()
    {
        string managerTypeName = this.GetType().Name;
        return $"{Name} - {managerTypeName} (Ranking: {Ranking:F2})";
    }

  
}