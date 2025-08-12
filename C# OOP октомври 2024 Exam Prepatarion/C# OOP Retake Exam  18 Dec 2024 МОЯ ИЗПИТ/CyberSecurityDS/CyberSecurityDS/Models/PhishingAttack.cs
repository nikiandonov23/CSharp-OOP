using CyberSecurityDS.Utilities.Messages;

namespace CyberSecurityDS.Models;

public class PhishingAttack:CyberAttack
{
    private string targetMail;





    public PhishingAttack(string attackName, int severityLevel, string targetMail) 
        : base(attackName, severityLevel)
    {
        this.TargetMail = targetMail;
    }






    public string TargetMail
    {
        get { return targetMail; }
        private set
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(ExceptionMessages.TargetMailRequired);
            }

            targetMail = value;
        }
    }

    public override string ToString()
    {
        return $"Attack: {AttackName}, Severity: {SeverityLevel} (Target Mail: {TargetMail})";
    }
}