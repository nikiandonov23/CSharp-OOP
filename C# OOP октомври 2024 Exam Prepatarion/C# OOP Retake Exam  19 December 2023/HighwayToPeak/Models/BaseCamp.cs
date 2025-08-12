using HighwayToPeak.Models.Contracts;

namespace HighwayToPeak.Models;

public class BaseCamp:IBaseCamp
{
    private List<string> residents;


    public BaseCamp()
    {
        residents = new List<string>();
    }


    public IReadOnlyCollection<string> Residents
    {
        get
        {
            return residents.OrderBy(x=>x).ToList().AsReadOnly();
        }
    }




    public void ArriveAtCamp(string climberName)
    {
        residents.Add(climberName);
    }

    public void LeaveCamp(string climberName)
    {
        residents.Remove(climberName);
    }
}