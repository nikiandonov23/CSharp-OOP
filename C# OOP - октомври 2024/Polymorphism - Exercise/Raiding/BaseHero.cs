namespace Raiding;

public abstract class BaseHero
{
    public string Name { get;  }

    protected  int  Power { get; set; }


    public abstract string CastAbility();

    protected BaseHero(string name, int power)
    {
        Name = name;
        Power = power;
    }
}