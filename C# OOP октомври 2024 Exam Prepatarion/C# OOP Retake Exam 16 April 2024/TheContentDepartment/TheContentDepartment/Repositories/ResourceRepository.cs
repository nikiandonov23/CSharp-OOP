using TheContentDepartment.Models.Contracts;
using TheContentDepartment.Repositories.Contracts;

namespace TheContentDepartment.Repositories;

public class ResourceRepository:IRepository<IResource>
{
    private List<IResource> models;

    public ResourceRepository()
    {
        models = new List<IResource>();
    }


    public IReadOnlyCollection<IResource> Models
    {
        get { return models.AsReadOnly(); }
    }

    public void Add(IResource model)
    {
        models.Add(model);
    }

    public IResource TakeOne(string modelName)
    {
        return models.FirstOrDefault(x => x.Name == modelName);
    }
}