using HighwayToPeak.Models.Contracts;
using HighwayToPeak.Utilities.Messages;

namespace HighwayToPeak.Models;

public  class Peak : IPeak   //zasq ne go pravw ABSTRACTEN ???!?!?!??!
{
    private string name;
    private int elevation;
    private string difficultyLevel;


    public Peak(string name, int elevation, string difficultyLevel)
    {
        this.Name = name;
        this.Elevation = elevation;
        this.DifficultyLevel = difficultyLevel;
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
                throw new ArgumentException(ExceptionMessages.PeakNameNullOrWhiteSpace);
            }
            name = value;
        }
    }
    public int Elevation
    {
        get { return elevation; }
        private set
        {
            if (value<0)
            {
                throw new ArgumentException(ExceptionMessages.PeakElevationNegative);
            }

            elevation = value;
        }
    }
    public string DifficultyLevel
    {
        get
        {
            return difficultyLevel;
        }
        private set
        {
          

            difficultyLevel = value;
        }
    }

    public override string ToString()
    {
        return $"Peak: {Name} -> Elevation: {Elevation}, Difficulty: {DifficultyLevel}";
    }
}