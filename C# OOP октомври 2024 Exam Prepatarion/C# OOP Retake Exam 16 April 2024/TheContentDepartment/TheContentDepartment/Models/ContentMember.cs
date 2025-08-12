using TheContentDepartment.Utilities.Messages;

namespace TheContentDepartment.Models;

public class ContentMember:TeamMember
{
    private string[] validPaths = new[] { "CSharp", "JavaScript", "Python", "Java" };


    public ContentMember(string name, string path) : base(name, path)
    {
        if (!validPaths.Contains(path))
        {
            throw new ArgumentException(ExceptionMessages.PathIncorrect,path);
        }


    }


    public override string ToString()
    {
        return $"{Name} - {Path} path. Currently working on {this.InProgress.Count} tasks.";
    }
}