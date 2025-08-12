namespace Shapes;

public class Rectangle:Shape
{
    private readonly double _height;
    private readonly double _width;

    public Rectangle(double height, double width)
    {
        _height = height;
        _width = width;
    }

    public override string Draw()
    {
        return $"Drawing {this.GetType().Name}";
    }



    public override double CalculatePerimeter()
    {
        return 2 * (this._height + this._width);
    }
    public override double CalculateArea()
    {
        return this._height * this._width;
    }
}