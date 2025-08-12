using TheContentDepartment.Models.Contracts;
using TheContentDepartment.Utilities.Messages;

namespace TheContentDepartment.Models;

public abstract class TeamMember:ITeamMember
{
    private string name;
    private string path;
    private List<string> inProgress;


    public TeamMember(string name, string path)
    {
        this.Name = name;
        this.Path = path;
        inProgress = new List<string>();
    }


    public string Name
    {
        get { return name; }
        private set
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(ExceptionMessages.NameNullOrWhiteSpace);
            }

            name = value;
        }
    }
    public string Path
    {
        get
        {
            return path;
        }
        protected set
        {


            path = value;
        }
    }
    public IReadOnlyCollection<string> InProgress
    {
        get
        {
            return inProgress.AsReadOnly();
        }
      
    }




    public void WorkOnTask(string resourceName)
    {
        inProgress.Add(resourceName);
    }

    public void FinishTask(string resourceName)
    {
        inProgress.Remove(resourceName);
    }



}