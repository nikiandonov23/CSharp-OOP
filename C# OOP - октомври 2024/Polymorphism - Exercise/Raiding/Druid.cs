using System.Runtime.CompilerServices;

namespace Raiding;

public class Druid:HealingHero
{
    private const int DefaultPower=80;

    public Druid(string name) : base(name, DefaultPower)
    {
    }
}