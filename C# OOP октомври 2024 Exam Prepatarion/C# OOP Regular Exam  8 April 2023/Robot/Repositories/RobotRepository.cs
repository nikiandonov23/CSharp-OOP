using System.Collections.Generic;
using System.Linq;
using RobotService.Models.Contracts;
using RobotService.Repositories.Contracts;

namespace RobotService.Repositories;

public class RobotRepository:IRepository<IRobot>
{
    private List<IRobot> models;


    public RobotRepository()
    {
        models = new List<IRobot>();
    }


    public IReadOnlyCollection<IRobot> Models()
    {
        return models.AsReadOnly();
    }
    public void AddNew(IRobot model)
    {
       models.Add(model);
    }
    public bool RemoveByName(string typeName)
    {
        var robotToBeRemoved = models.FirstOrDefault(x => x.GetType().Name == typeName);
        if (robotToBeRemoved!=null)
        {
            models.Remove(robotToBeRemoved);
            return true;

        }
        return false;
    }
    public IRobot FindByStandard(int interfaceStandard)
    {
        return models.FirstOrDefault(x => x.InterfaceStandards.Contains(interfaceStandard));

    }
}