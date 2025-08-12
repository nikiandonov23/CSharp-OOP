namespace Cars;

public class Tesla:ICar ,IElectricCar
{
    public string Model { get; set; }
    public string Color { get; set; }
    public string Start()
    {
        return "Engine start";
    }

    public string Stop()
    {
        return "Breaaak!";
    }

    public int Battery { get; set; }

    public Tesla(string model, string color, int battery)
    {
        Model = model;
        Color = color;
        Battery = battery;
    }

    public override string ToString()
    {
        return $"{this.Color} {this.GetType().Name} {this.Model} with {this.Battery} Batteries";
    }
}