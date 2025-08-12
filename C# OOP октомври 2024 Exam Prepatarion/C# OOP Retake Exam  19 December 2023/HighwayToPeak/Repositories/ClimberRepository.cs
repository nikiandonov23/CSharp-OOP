using HighwayToPeak.Models.Contracts;
using HighwayToPeak.Repositories.Contracts;

namespace HighwayToPeak.Repositories;

public class ClimberRepository:IRepository<IClimber>
{
    private List<IClimber> all;


    public ClimberRepository()
    {
        all = new List<IClimber>();
    }


    public IReadOnlyCollection<IClimber> All
    {
        get
        {
            return all.AsReadOnly();
        }
    }

    public void Add(IClimber model)
    {
        all.Add(model);
    }

    public IClimber Get(string name)
    {
        return all.FirstOrDefault(x => x.Name == name);
    }
}