using System.Collections.Generic;
using System.Linq;
using Handball.Models.Contracts;
using Handball.Repositories.Contracts;

namespace Handball.Repositories;

public class TeamRepository:IRepository<ITeam>
{
    private List<ITeam> models;

    public TeamRepository()
    {
        models=new List<ITeam>();
    }


    public IReadOnlyCollection<ITeam> Models
    {
        get { return models.AsReadOnly(); }
    }

    public void AddModel(ITeam model)
    {
        models.Add(model);
    }

    public bool RemoveModel(string name)
    {
        var modelToBERemoved = models.FirstOrDefault(x => x.Name == name);
        if (modelToBERemoved != null)
        {
            models.Remove(modelToBERemoved);
            return true;
        }
        return false;
    }

    public bool ExistsModel(string name)
    {
        var modelToBEChecked = models.FirstOrDefault(x => x.Name == name);
        if (modelToBEChecked != null)
        {
            return true;
        }
        return false;
    }

    public ITeam GetModel(string name)
    {
        var modelToBEChecked = models.FirstOrDefault(x => x.Name == name);

        if (modelToBEChecked != null)
        {
            return modelToBEChecked;
        }
        return null;
    }
}