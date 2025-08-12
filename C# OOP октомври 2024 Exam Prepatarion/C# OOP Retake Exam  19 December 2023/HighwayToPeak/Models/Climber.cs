using System.Text;
using HighwayToPeak.Models.Contracts;
using HighwayToPeak.Utilities.Messages;

namespace HighwayToPeak.Models;

public abstract class Climber:IClimber
{
    private string name;
    private int stamina;
    private List<string> conqueredPeaks;


    public Climber(string name, int stamina)
    {
        this.Name = name;
        this.Stamina = stamina;
        conqueredPeaks = new List<string>();
    }


    public string Name
    {
        get { return name; }
        private set
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(ExceptionMessages.ClimberNameNullOrWhiteSpace);
            }

            name = value;
        }
    }
    public int Stamina
    {
        get { return stamina; }
        protected set
        {
            if (value>10)
            {
                stamina = 10;
            }

            else if (value<0)
            {
                stamina = 0;
            }

            else
            {
                stamina = value;
            }
        }
    }
    public IReadOnlyCollection<string> ConqueredPeaks
    {
        get { return conqueredPeaks.AsReadOnly(); }
    }


    public void Climb(IPeak peak)
    {
        if (peak.DifficultyLevel== "Extreme")
        {
            this.Stamina -= 6;
        }

        if (peak.DifficultyLevel== "Hard")
        {
            this.Stamina -= 4;
        }
        if (peak.DifficultyLevel == "Moderate")
        {
            this.Stamina -= 2;
        }


        if (!conqueredPeaks.Contains(peak.Name))
        {
            conqueredPeaks.Add(peak.Name);
        }
    }

    public abstract void Rest(int daysCount);


    public override string ToString()
    {
        StringBuilder result = new StringBuilder();

        result.AppendLine($"{this.GetType().Name} - Name: {Name}, Stamina: {Stamina}");
        if (this.ConqueredPeaks.Count>0)
        {
            result.AppendLine($"Peaks conquered: {ConqueredPeaks.Count}");
        }

        if (this.ConqueredPeaks.Count==0)
        {
            result.AppendLine($"Peaks conquered: no peaks conquered");
        }


        return result.ToString().Trim();
    }
}