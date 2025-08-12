using System.Collections.Generic;
using System.Linq;
using RobotService.Models.Contracts;
using RobotService.Repositories.Contracts;

namespace RobotService.Repositories;

public class SupplementRepository:IRepository<ISupplement>
{
    private List<ISupplement> models;


    public SupplementRepository()
    {
        models = new List<ISupplement>();
    }


    public IReadOnlyCollection<ISupplement> Models()
    {
        return models.AsReadOnly();
    }
    public void AddNew(ISupplement model)
    {
        models.Add(model);
    }

    public bool RemoveByName(string typeName)
    {
        var modelToBeRemoved = models.FirstOrDefault(x => x.GetType().Name == typeName);
        if (modelToBeRemoved!=null)
        {
            models.Remove(modelToBeRemoved);
            return true;
        }
        return false;
    }

    public ISupplement FindByStandard(int interfaceStandard)
    {
        return models.FirstOrDefault(x => x.InterfaceStandard == interfaceStandard);
    }
}