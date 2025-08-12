using CyberSecurityDS.Core.Contracts;
using CyberSecurityDS.Models;
using CyberSecurityDS.Models.Contracts;
using CyberSecurityDS.Utilities.Messages;
using System.Text;

namespace CyberSecurityDS.Core;

public class Controller:IController
{
    private ISystemManager systemManager = new SystemManager();

    private string[] validAttackTypes = new[] { nameof(PhishingAttack), nameof(MalwareAttack) };
    private string[] validSoftwareeTypes = new[] { nameof(Firewall), nameof(Antivirus) };

    public string AddCyberAttack(string attackType, string attackName, int severityLevel, string extraParam)   //дава 42
    {
        if (!validAttackTypes.Contains(attackType))
        {
            return String.Format(OutputMessages.TypeInvalid, attackType);
        }


        if (systemManager.CyberAttacks.Exists(attackName))
        {
            return String.Format(OutputMessages.EntryAlreadyExists, attackName);
        }


        ICyberAttack attackToBeAdded = null;
        switch (attackType)
        {
            case $"{nameof(PhishingAttack)}":
                attackToBeAdded = new PhishingAttack(attackName, severityLevel, extraParam);
                break;

            case $"{nameof(MalwareAttack)}":
                attackToBeAdded = new MalwareAttack(attackName, severityLevel, extraParam);
                break;
        }

        systemManager.CyberAttacks.AddNew(attackToBeAdded);
        return String.Format(OutputMessages.EntryAddedSuccessfully, attackType, attackName);

    }
    public string AddDefensiveSoftware(string softwareType, string softwareName, int effectiveness)
    {
        if (!validSoftwareeTypes.Contains(softwareType))
        {
            return String.Format(OutputMessages.TypeInvalid, softwareType);
        }

        if (systemManager.DefensiveSoftwares.Exists(softwareName))
        {
            return String.Format(OutputMessages.EntryAlreadyExists, softwareName);
        }


        IDefensiveSoftware toBeAdded = null;
        switch (softwareType)
        {
            case $"{nameof(Firewall)}":
                toBeAdded = new Firewall(softwareName, effectiveness);
                break;

            case $"{nameof(Antivirus)}":
                toBeAdded = new Antivirus(softwareName, effectiveness);
                break;
        }


        systemManager.DefensiveSoftwares.AddNew(toBeAdded);

        return String.Format(OutputMessages.EntryAddedSuccessfully, softwareType, softwareName);
    }    //дава 30
    public string AssignDefense(string cyberAttackName, string defensiveSoftwareName)   //дава 24
    {
        if (!systemManager.CyberAttacks.Exists(cyberAttackName))
        {
            return String.Format(OutputMessages.EntryNotFound, cyberAttackName);
        }

        if (!systemManager.DefensiveSoftwares.Exists(defensiveSoftwareName))
        {
            return String.Format(OutputMessages.EntryNotFound, defensiveSoftwareName);
        }

        var currentCyberAttack = systemManager.CyberAttacks.GetByName(cyberAttackName);
        var currentDefenciveSoftware = systemManager.DefensiveSoftwares.GetByName(defensiveSoftwareName);


        foreach (var sftw in systemManager.DefensiveSoftwares.Models)
        {
            if (sftw.AssignedAttacks.Contains(currentCyberAttack.AttackName))
            {
                return String.Format(OutputMessages.AttackAlreadyAssigned, cyberAttackName,sftw.Name);
                break;
            } 
        }

        currentDefenciveSoftware.AssignAttack(currentCyberAttack.AttackName);

        return String.Format(OutputMessages.AttackAssignedSuccessfully, currentCyberAttack.AttackName,
            currentDefenciveSoftware.Name);

    }
    public string MitigateAttack(string cyberAttackName)
    {
        if (!systemManager.CyberAttacks.Exists(cyberAttackName))
        {
            return String.Format(OutputMessages.EntryNotFound, cyberAttackName);
        }

        var currentCyberAttack = systemManager.CyberAttacks.GetByName(cyberAttackName);

        if (currentCyberAttack.Status==true)
        {
            return String.Format(OutputMessages.AttackAlreadyMitigated, cyberAttackName);
        }



        bool isAsigned = false;
        foreach (var stwr in systemManager.DefensiveSoftwares.Models)
        {

            if (stwr.AssignedAttacks.Contains(cyberAttackName))
            {
                isAsigned = true;
                break;

            }
        }

        if (!isAsigned)
        {
            return String.Format(OutputMessages.AttackNotAssignedYet, cyberAttackName);
        }

        var currentDefenciveSoftware=systemManager.DefensiveSoftwares.Models
            .FirstOrDefault(x=>x.AssignedAttacks.Contains(cyberAttackName));



        if (currentDefenciveSoftware.GetType().Name==nameof(Firewall))
        {
            if (currentCyberAttack.GetType().Name!=nameof(MalwareAttack))
            {
                return String.Format(OutputMessages.CannotMitigateDueToCompatibility,
                    currentDefenciveSoftware.GetType().Name, currentCyberAttack.GetType().Name);
            }
        }

        if (currentDefenciveSoftware.GetType().Name==nameof(Antivirus))
        {
            if (currentCyberAttack.GetType().Name!=nameof(PhishingAttack))
            {
                return String.Format(OutputMessages.CannotMitigateDueToCompatibility,
                    currentDefenciveSoftware.GetType().Name, currentCyberAttack.GetType().Name);
            }
        }

        int effectivnes = currentDefenciveSoftware.Effectiveness;
        int severity = currentCyberAttack.SeverityLevel;
        if (effectivnes>=severity)
        {
            currentCyberAttack.MarkAsMitigated();
            return String.Format(OutputMessages.AttackMitigatedSuccessfully, cyberAttackName);
        }
        else
        {
            return String.Format(OutputMessages.SoftwareNotEffectiveEnough, cyberAttackName,
                currentDefenciveSoftware.Name);
        }



    }     // дава много :D:D:D








    public string GenerateReport()
    {
        var result = new StringBuilder();

        var sortedSoftwares = systemManager.DefensiveSoftwares.Models
            .OrderBy(software => software.Name)
            .ToList();

        result.AppendLine("Security:");
        foreach (var software in sortedSoftwares)
        {
            result.AppendLine(software.ToString());



        }





        var mitigatedAttacks = systemManager.CyberAttacks.Models
            .Where(attack => attack.Status == true)
            .OrderBy(attack => attack.AttackName)
            .ToList();

        var pendingAttacks = systemManager.CyberAttacks.Models
            .Where(attack => attack.Status == false)
            .OrderBy(attack => attack.AttackName)
            .ToList();

        result.AppendLine("Threads:");

        result.AppendLine("-Mitigated:");
        foreach (var attack in mitigatedAttacks)
        {


            result.AppendLine(attack.ToString().Trim());
        }

        result.AppendLine("-Pending:");
        foreach (var attack in pendingAttacks)
        {

            result.AppendLine(attack.ToString().Trim());



        }

        return result.ToString().Trim(); 
    }

}