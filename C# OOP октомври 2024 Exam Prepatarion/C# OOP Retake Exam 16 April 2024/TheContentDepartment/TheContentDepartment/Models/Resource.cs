using TheContentDepartment.Models.Contracts;
using TheContentDepartment.Utilities.Messages;

namespace TheContentDepartment.Models;

public abstract class Resource:IResource
{
    private string name;
    private string creator;
    private int priority;
    private bool isTested;
    private bool isApproved;

    public Resource(string name, string creator, int priority)
    {
        this.Name = name;
        this.Creator = creator;
        this.Priority = priority;
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
    public string Creator
    {
        get
        {
            return creator;
        }
        private set
        {
            creator = value;
        }
    }



    public int Priority
    {
        get
        {
            return priority;
        }
        set
        {
            priority = value;
        }
    }

    public bool IsTested
    {
        get { return isTested; }
        private set
        {
            isTested = value;
        }
    }



    public bool IsApproved
    {
        get
        {
            return isApproved;
        }
        private set
        {
            isApproved = value;
        }
    }

    public void Test()
    {
        IsTested = !isTested;
    }

    public void Approve()
    {
        IsApproved=true;
    }

    public override string ToString()
    {
        return $"{Name} ({this.GetType().Name}), Created By: {Creator}";
    }
}