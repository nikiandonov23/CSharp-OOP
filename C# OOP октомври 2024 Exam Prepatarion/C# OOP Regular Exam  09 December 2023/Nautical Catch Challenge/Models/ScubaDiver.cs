namespace NauticalCatchChallenge.Models;

public class ScubaDiver:Diver
{
    private int maxValueOxygen = 540;
    public ScubaDiver(string name) : base(name, 540)
    {
    }

    public override void Miss(int TimeToCatch)
    {
        OxygenLevel -= (int)Math.Round(TimeToCatch*0.3, MidpointRounding.AwayFromZero);
    }

    public override void RenewOxy()
    {
        OxygenLevel = maxValueOxygen;
    }
}