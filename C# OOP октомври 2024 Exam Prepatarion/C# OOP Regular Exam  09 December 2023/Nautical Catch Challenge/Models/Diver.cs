using NauticalCatchChallenge.Models.Contracts;
using NauticalCatchChallenge.Utilities.Messages;

namespace NauticalCatchChallenge.Models;

public abstract class Diver : IDiver
{
    private string name;
    private int oxygenLevel;
    private List<string> caughtFish;
    private double competitionPoints;
    private bool hasHealthIssues;

    public Diver(string name, int oxygenLevel)
    {
        this.Name = name;
        this.OxygenLevel = oxygenLevel;
        caughtFish = new List<string>();
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
                throw new ArgumentException(ExceptionMessages.DiversNameNull);
            }
            name = value;
        }
    }
    public int OxygenLevel
    {
        get
        {
            return oxygenLevel;
        }
        protected set
        {
            if (value <= 0)
            {
                oxygenLevel = 0;
                hasHealthIssues = true;
            }
            oxygenLevel = value;
        }
    }     //    !!!! TUKA IMA NQKVA DOPULNITELNA LOGIKA I PROTECTED SET !!!!
    public IReadOnlyCollection<string> Catch
    {
        get
        {
            return caughtFish.AsReadOnly();
        }

    }

    public double CompetitionPoints
    {
        get { return competitionPoints; }
        
    }

    public bool HasHealthIssues
    {
        get { return hasHealthIssues; }
        private set { hasHealthIssues = value; }

    }

    public void Hit(IFish fish)
    {
        this.OxygenLevel -= fish.TimeToCatch;
        this.caughtFish.Add(fish.Name);
        competitionPoints = Math.Round(competitionPoints + fish.Points, 1);
    }

    public abstract void Miss(int TimeToCatch);



    public abstract void RenewOxy();
  

    public void UpdateHealthStatus()
    {
        if (HasHealthIssues)
        {
            HasHealthIssues = false;
        }

        HasHealthIssues = false;
    }

    public override string ToString()
    {
        return
            $"Diver [ Name: {Name}, Oxygen left: {OxygenLevel}, Fish caught: {caughtFish.Count}, Points earned: {CompetitionPoints} ]";
    }
}