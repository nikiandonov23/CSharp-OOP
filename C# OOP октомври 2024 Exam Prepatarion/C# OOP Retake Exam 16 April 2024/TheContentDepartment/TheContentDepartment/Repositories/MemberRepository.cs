using TheContentDepartment.Models.Contracts;
using TheContentDepartment.Repositories.Contracts;

namespace TheContentDepartment.Repositories;

public class MemberRepository:IRepository<ITeamMember>
{
    private List<ITeamMember> models;

    public MemberRepository()
    {
        models = new List<ITeamMember>();
    }


    public IReadOnlyCollection<ITeamMember> Models
    {
        get { return models.AsReadOnly(); }
    }

    public void Add(ITeamMember model)
    {
        models.Add(model);
    }

    public ITeamMember TakeOne(string modelName)
    {
        return models.FirstOrDefault(x => x.Name == modelName);
    }
}