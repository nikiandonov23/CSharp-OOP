using CarDealership.Models.Contracts;
using CarDealership.Repositories.Contracts;

namespace CarDealership.Repositories;

public class CustomerRepository:IRepository<ICustomer>
{
    private List<ICustomer> models;


    public CustomerRepository()
    {
        this.models = new List<ICustomer>();
    }


    public IReadOnlyCollection<ICustomer> Models
    {
        get { return models.AsReadOnly(); }
    }

    public void Add(ICustomer model)
    {
        models.Add(model);
    }

    public bool Remove(string text)
    {
        var modelToBeRemoved = models.FirstOrDefault(x => x.Name == text);
        if (modelToBeRemoved!=null)
        {
            models.Remove(modelToBeRemoved);
            return true;
        }

        return false;
    }

    public bool Exists(string text)
    {
        var modelToBeReturned = models.FirstOrDefault(x => x.Name == text);
        if (modelToBeReturned!=null)
        {
            return true;
        }

        return false;
    }

    public ICustomer Get(string text)
    {
        return models.FirstOrDefault(x => x.Name == text);
    }
}