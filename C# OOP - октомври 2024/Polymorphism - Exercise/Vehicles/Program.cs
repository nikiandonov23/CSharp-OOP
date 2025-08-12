using System.Text;
using Vehicles.Models;
using Vehicles.Models.Interfaces;

namespace Vehicles
{
    public class Program
    {
        public static void Main()
        {
            IVehicle car = new Car(15, 0.3);
            car.Drive(9);
            Console.WriteLine(car.ToString());

            IVehicle truck = new Truck(100, 0.9);
            truck.Drive(10);
            Console.WriteLine(truck.ToString());
        }
    }
}
