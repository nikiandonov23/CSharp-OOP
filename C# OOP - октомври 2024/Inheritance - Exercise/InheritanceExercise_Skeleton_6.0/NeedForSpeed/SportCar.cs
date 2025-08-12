namespace NeedForSpeed;

public class SportCar : Car
{
    public override double FuelConsumption  => 10;

    public SportCar(int horsePower, double fuel) : base(horsePower, fuel)
    {
    }
}