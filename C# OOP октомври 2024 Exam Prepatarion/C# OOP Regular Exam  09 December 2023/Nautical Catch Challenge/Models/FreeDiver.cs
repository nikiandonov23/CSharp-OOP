namespace NauticalCatchChallenge.Models;

public class FreeDiver:Diver
{
    private int maxValueOxygen = 120;
    public FreeDiver(string name) : base(name, 120)
    {
    }

    public override void Miss(int TimeToCatch)
    {
        OxygenLevel -= (int)Math.Round(TimeToCatch*0.6, MidpointRounding.AwayFromZero);
    }

    public override void RenewOxy()
    {
        OxygenLevel = maxValueOxygen;
    }
}