using System.Collections.Generic;
using System.Linq;
using Handball.Models.Contracts;
using Handball.Repositories.Contracts;

namespace Handball.Repositories;

public class PlayerRepository:IRepository<IPlayer>
{
    private List<IPlayer> models;



    public PlayerRepository()
    {
        models=new List<IPlayer>();
    }







    public IReadOnlyCollection<IPlayer> Models
    {
        get { return models.AsReadOnly(); }
    }



    public void AddModel(IPlayer model)
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
        if (modelToBEChecked!=null)
        {
            return true;
        }
        return false;

    }

    public IPlayer GetModel(string name)
    {
        var modelToBEChecked = models.FirstOrDefault(x => x.Name == name);

        if (modelToBEChecked != null)
        {
            return modelToBEChecked;
        }
        return null;
    }
}