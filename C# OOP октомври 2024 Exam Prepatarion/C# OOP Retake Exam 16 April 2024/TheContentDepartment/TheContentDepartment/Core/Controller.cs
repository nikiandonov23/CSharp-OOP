using System.Text;
using TheContentDepartment.Core.Contracts;
using TheContentDepartment.Models;
using TheContentDepartment.Models.Contracts;
using TheContentDepartment.Repositories;
using TheContentDepartment.Utilities.Messages;

namespace TheContentDepartment.Core;

public class Controller : IController
{
    private string[] validMemberTypes = new[] { nameof(TeamLead), nameof(ContentMember) };
    private string[] validResourceTypes = new[] { nameof(Exam), nameof(Workshop), nameof(Presentation) };


    private ResourceRepository resources = new ResourceRepository();
    private MemberRepository members = new MemberRepository();



    public string JoinTeam(string memberType, string memberName, string path)
    {
        if (!validMemberTypes.Contains(memberType))
        {

            return String.Format(OutputMessages.MemberTypeInvalid, memberType);
        }

        if (members.Models.Any(x => x.Path == path))
        {
            return String.Format(OutputMessages.PositionOccupied);
        }

        if (members.TakeOne(memberName)!=null)
        {
            return String.Format(OutputMessages.MemberExists, memberName);
        }

        ITeamMember memberToBeAdded = null;
        if (path=="Master")
        {
            memberToBeAdded = new TeamLead(memberName, path);
        }

        if (path== "CSharp"|| path == "JavaScript" || path == "Python" || path == "Java")
        {
            memberToBeAdded = new ContentMember(memberName, path);
        }
        members.Add(memberToBeAdded);
        return String.Format(OutputMessages.MemberJoinedSuccessfully, memberName);
    }    //dava 32 ot 150
    public string CreateResource(string resourceType, string resourceName, string path)   //ДАВА 32 ОТ 150
    {
        if (!validResourceTypes.Contains(resourceType))
        {
            return String.Format(OutputMessages.ResourceTypeInvalid, resourceType);
        }



        ITeamMember appropriateMember = null;
        foreach (var membr in members.Models)
        {
            if (membr.Path == path)
            {
                if (membr.InProgress.Contains(resourceName))
                {
                    return String.Format(OutputMessages.ResourceExists, resourceName);
                }
                appropriateMember = membr;
                break; 
            }
        }

        if (appropriateMember == null)
        {
            return String.Format(OutputMessages.NoContentMemberAvailable, resourceName);
        }


        IResource resourceToBeAdded = null;
        if (resourceType==nameof(Exam))
        {
            resourceToBeAdded = new Exam(resourceName, appropriateMember.Name);
        }
        if (resourceType == nameof(Workshop))
        {
            resourceToBeAdded = new Workshop(resourceName, appropriateMember.Name);
        }
        if (resourceType == nameof(Presentation))
        {
            resourceToBeAdded = new Presentation(resourceName, appropriateMember.Name);
        }
        
        appropriateMember.WorkOnTask(resourceToBeAdded.Name);
        resources.Add(resourceToBeAdded);
        return String.Format(OutputMessages.ResourceCreatedSuccessfully, appropriateMember.Name, resourceType,
            resourceName);

    }
    public string LogTesting(string memberName)   //ДАВА 13
    {
        if (members.TakeOne(memberName)==null)
        {
            return String.Format(OutputMessages.WrongMemberName);
        }

        var ListResources = resources.Models
            .Where(x => x.IsTested == false)
            .Where(x => x.Creator == memberName)
            .ToList();
        var sortedResources=ListResources.OrderBy(x => x.Priority).ToList();


        if (!sortedResources.Any())
        {
            return String.Format(OutputMessages.NoResourcesForMember, memberName);
        }

        var highestPriorityResource = sortedResources.First();

        var teamLead = members.Models.FirstOrDefault(x => x.GetType().Name == nameof(TeamLead));
        var creator = members.TakeOne(memberName);
        creator.FinishTask(highestPriorityResource.Name);
        teamLead.WorkOnTask(highestPriorityResource.Name);
        highestPriorityResource.Test();
        return String.Format(OutputMessages.ResourceTested,highestPriorityResource.Name);

    } 




    public string ApproveResource(string resourceName, bool isApprovedByTeamLead)
    {
        var currentResource = resources.TakeOne(resourceName);
        if (currentResource.IsTested==false)
        {
            return String.Format(OutputMessages.ResourceNotTested, resourceName);
        }

        var teamLead = members.Models.FirstOrDefault(x=>x.GetType().Name==nameof(TeamLead));
        if (isApprovedByTeamLead==true)
        {
            currentResource.Approve();
            teamLead.FinishTask(resourceName);
            return String.Format(OutputMessages.ResourceApproved, teamLead.Name, resourceName);
        }

        

            currentResource.Test();
            return String.Format(OutputMessages.ResourceReturned, teamLead.Name, resourceName);

        
        

    }  //ДАВА 7

    public string DepartmentReport()
    {
        var approvedResources = resources.Models
            .Where(r => r.IsApproved)
            .ToList();

        StringBuilder report = new StringBuilder();
        report.AppendLine("Finished Tasks:");

        foreach (var resource in approvedResources)
        {
            report.AppendLine($"--{resource}");
        }

        report.AppendLine("Team Report:");

        var teamLead = members.Models.FirstOrDefault(x => x is TeamLead);
        if (teamLead != null)
        {
            report.AppendLine($"--{teamLead.Name} (TeamLead) - Currently working on {teamLead.InProgress.Count} tasks.");
        }

        
        var contentMembers = members.Models.Where(x => x is ContentMember).ToList();
        foreach (var contentMember in contentMembers)
        {
            report.AppendLine($"{contentMember.Name} - {contentMember.Path} path. Currently working on {contentMember.InProgress.Count} tasks.");
        }

        return report.ToString().Trim();
    }
}