using CyberSecurityDS.Models.Contracts;
using CyberSecurityDS.Utilities.Messages;

namespace CyberSecurityDS.Models;

public abstract class DefensiveSoftware:IDefensiveSoftware
{
    private string name;
    private int effectiveness;
    private List<string> assignedAttacks;


    public DefensiveSoftware(string name, int effectiveness)
    {
        this.Name = name;
        this.Effectiveness = effectiveness;
        this.assignedAttacks = new List<string>();
    }


    public string Name
    {
        get
        {
            return name;
        }
        private set
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(ExceptionMessages.SoftwareNameRequired);
            }

            name = value;
        }
    }

    public int Effectiveness
    {
        get { return effectiveness; }
        private set
        {
            if (value < 0)
            {
                throw new ArgumentException(ExceptionMessages.EffectivenessNegative);
            }
            else if (value == 0)
            {
                effectiveness = 1;
            }
            else if (value > 10)
            {
                effectiveness = 10;
            }
            else
            {
                effectiveness = value;
            }

        }
    }

    public IReadOnlyCollection<string> AssignedAttacks
    {
        get { return assignedAttacks.AsReadOnly(); }
    }

    public void AssignAttack(string attackName)
    {
        assignedAttacks.Add(attackName);
    }


    public override string ToString()
    {
        string assignedAttacksStr;

        if (AssignedAttacks.Count == 0)
        {
            assignedAttacksStr = "[None]"; 
        }
        else
        {
            assignedAttacksStr = string.Join(", ", AssignedAttacks);
        }

        return $"Defensive Software: {Name}, Effectiveness: {Effectiveness}, Assigned Attacks: {assignedAttacksStr}";
    }

}