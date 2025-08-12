namespace HighwayToPeak.Models;

public class OxygenClimber:Climber
{
    private bool isAllowedToGoExtreme = true;
    public OxygenClimber(string name) : base(name, 10)
    {
    }

    public override void Rest(int daysCount)
    {
        this.Stamina += daysCount * 1;
    }
}