namespace TheContentDepartment.Models;

public class Workshop:Resource
{
    public Workshop(string name, string creator) : base(name, creator, 2)
    {
    }
}