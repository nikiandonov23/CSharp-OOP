using CyberSecurityDS.Models.Contracts;
using CyberSecurityDS.Utilities.Messages;

namespace CyberSecurityDS.Models;

public abstract class CyberAttack:ICyberAttack
{
    private string attackName;
    private int severityLevel;
    private bool status=false;


    public CyberAttack(string attackName, int severityLevel)
    {
        this.AttackName = attackName;
        this.SeverityLevel = severityLevel;
    }


    public string AttackName
    {
        get
        {
            return attackName;
        }
        private set
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(ExceptionMessages.CyberAttackNameRequired);
            }

            attackName = value;
        }
    }
    public int SeverityLevel
    {
        get { return severityLevel; }
        private set
        {
            if (value<0)
            {
                throw new ArgumentException(ExceptionMessages.SeverityLevelNegative);
            }
            else if (value==0)
            {
                severityLevel = 1;
            }
            else if (value>10)
            {
                severityLevel = 10;
            }
            else
            {
                severityLevel = value;
            }
            
        }
    }
    public bool Status
    {
        get
        {
            return status;
        }
        private set
        {
          

            status = value;
        }
    }

    public void MarkAsMitigated()
    {
        this.Status = true;
    }
}