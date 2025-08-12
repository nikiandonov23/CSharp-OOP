using HighwayToPeak.Models.Contracts;
using HighwayToPeak.Repositories.Contracts;

namespace HighwayToPeak.Repositories;

public class PeakRepository:IRepository<IPeak>
{
    private List<IPeak> all;

    public PeakRepository()
    {
        all = new List<IPeak>();
    }


    public IReadOnlyCollection<IPeak> All
    {
        get { return all.AsReadOnly(); }
    }

    public void Add(IPeak model)
    {
        all.Add(model);
    }

    public IPeak Get(string name)
    {
        return all.FirstOrDefault(x => x.Name == name);
    }
}