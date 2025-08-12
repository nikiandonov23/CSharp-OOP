using System.Runtime.CompilerServices;

namespace Raiding;

public class Warrior:HittingHero
{
    private const int DefaultPower = 100;

    public Warrior(string name) : base(name, DefaultPower)
    {
    }
}