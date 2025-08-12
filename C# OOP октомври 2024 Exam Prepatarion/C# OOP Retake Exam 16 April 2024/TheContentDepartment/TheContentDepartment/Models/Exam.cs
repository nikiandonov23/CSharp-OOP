namespace TheContentDepartment.Models;

public class Exam:Resource
{
    public Exam(string name, string creator) : base(name, creator, 1)
    {
    }
}