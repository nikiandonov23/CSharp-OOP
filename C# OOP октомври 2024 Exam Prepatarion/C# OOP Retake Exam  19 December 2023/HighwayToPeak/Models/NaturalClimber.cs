namespace HighwayToPeak.Models;

public class NaturalClimber:Climber
{
    private bool isAllowedToGoExtreme=false;
    public NaturalClimber(string name ) : base(name, 6)
    {
    }

    public override void Rest(int daysCount)
    {
        this.Stamina += daysCount * 2;
    }
}