namespace Shapes;

public class Circle:Shape
{

    private readonly double _radius;


    public Circle(double radius)
    {
        _radius = radius;
    }

    public override string Draw()
    {
        return $"Drawing {this.GetType().Name}";
    }




    public override double CalculatePerimeter()
    {
        return 2*Math.PI*this._radius;
    }
    public override double CalculateArea()
    {
        return Math.PI * Math.Pow(this._radius, 2);
    }
}