using CyberSecurityDS.Models.Contracts;
using CyberSecurityDS.Repositories.Contracts;

namespace CyberSecurityDS.Repositories;

public class CyberAttackRepository:IRepository<ICyberAttack>
{
    private List<ICyberAttack> models;


    public CyberAttackRepository()
    {
        models = new List<ICyberAttack>();
    }


    public IReadOnlyCollection<ICyberAttack> Models
    {
        get { return models.AsReadOnly(); }
    }

    public void AddNew(ICyberAttack model)
    {
        models.Add(model);
    }

    public ICyberAttack GetByName(string name)
    {
        return models.FirstOrDefault(x => x.AttackName == name);
    }

    public bool Exists(string name)
    {
        var modelToBeChecked = models.FirstOrDefault(x => x.AttackName == name);
        if (modelToBeChecked != null)
        {
            return true;
        }

        return false;
    }
}