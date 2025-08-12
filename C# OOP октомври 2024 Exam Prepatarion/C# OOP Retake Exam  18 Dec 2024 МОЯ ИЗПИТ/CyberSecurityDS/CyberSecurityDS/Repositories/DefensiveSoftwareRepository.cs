using CyberSecurityDS.Models.Contracts;
using CyberSecurityDS.Repositories.Contracts;

namespace CyberSecurityDS.Repositories;

public class DefensiveSoftwareRepository:IRepository<IDefensiveSoftware>
{
    private List<IDefensiveSoftware> models;


    public DefensiveSoftwareRepository()
    {
        this.models = new List<IDefensiveSoftware>();
    }


    public IReadOnlyCollection<IDefensiveSoftware> Models
    {
        get { return models.AsReadOnly(); }
    }

    public void AddNew(IDefensiveSoftware model)
    {
        models.Add(model);
    }

    public IDefensiveSoftware GetByName(string name)
    {
        return models.FirstOrDefault(x => x.Name == name);
    }

    public bool Exists(string name)
    {
        var modelToBeChecked=models.FirstOrDefault(x => x.Name == name);
        if (modelToBeChecked!=null)
        {
            return true;
        }

        return false;
    }
}