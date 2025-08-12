using BlackFriday.Models.Contracts;
using BlackFriday.Repositories.Contracts;

namespace BlackFriday.Repositories;

public class ProductRepository:IRepository<IProduct>
{
    private List<IProduct> models;

    public ProductRepository()
    {
        models = new List<IProduct>();
    }


    public IReadOnlyCollection<IProduct> Models
    {
        get { return models.AsReadOnly(); }
    }

    public void AddNew(IProduct model)
    {
        models.Add(model);
    }

    public IProduct GetByName(string name)
    {
        return models.FirstOrDefault(x => x.ProductName == name);
    }




    public bool Exists(string name)
    {
        var productToBeReturned = models.FirstOrDefault(x => x.ProductName == name);
        if (productToBeReturned!=null)
        {
            return true;
        }

        return false;
    }
}