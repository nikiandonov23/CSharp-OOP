namespace Shapes
{
    public class StartUp
    {
        public static void Main()
        {

            Circle circle = new Circle(3);
            Console.WriteLine($"Circle area is {circle.CalculateArea()}");
            Console.WriteLine($"Circle perimeter is {circle.CalculatePerimeter()}");
            Console.WriteLine($"{circle.Draw()}");


            Rectangle rect = new Rectangle(10,8);
            Console.WriteLine($"Circle area is {rect.CalculateArea()}");
            Console.WriteLine($"Circle perimeter is {rect.CalculatePerimeter()}");
            Console.WriteLine($"{rect.Draw()}");




        }
    }
}
