using FootballManager.Models.Contracts;
using FootballManager.Repositories.Contracts;

namespace FootballManager.Repositories;

public class TeamRepository: IRepository<ITeam>
{
    private List<ITeam> models;
    private int capacity=10;
    public TeamRepository()
    {
        models=new List<ITeam>();    //inizializiram models zashtoto v na4aloto e NULL
    }



    public IReadOnlyCollection<ITeam> Models 
    {
        get
        {
            return models.AsReadOnly();
        }
      
    }

    public int Capacity
    {
        get
        {
            return capacity;
        }
      
    }

    public void Add(ITeam model)
    {
        if (models.Count>=capacity)
        {
            return;
        }
        models.Add(model);
    }

    public bool Remove(string name)
    {
        var modelToBeRemoved = models.FirstOrDefault(x => x.Name == name);
        if (modelToBeRemoved != null)
        {
            models.Remove(modelToBeRemoved);
            return true;

        }

        return false;
    }

    public bool Exists(string name)
    {
        var modelToBeCHecked=models.FirstOrDefault(x => x.Name == name);
        if (modelToBeCHecked!=null)
        {
            return true;
        }

        return false;
    }

    public ITeam Get(string name)
    {
        var modelToBeReturned=models.FirstOrDefault(x => x.Name == name);
        if (modelToBeReturned!=null)
        {
            return modelToBeReturned;
        }

        return null;
    }
}