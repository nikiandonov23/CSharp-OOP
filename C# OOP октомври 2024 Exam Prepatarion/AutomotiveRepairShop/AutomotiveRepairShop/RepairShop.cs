using System.Text;

namespace AutomotiveRepairShop
{
    public class RepairShop
    {
        private int capacity;

        public int Capacity
        {
            get { return capacity; }
            set { capacity = value; }
        }



        private List<Vehicle> vehicles;

        public List<Vehicle> Vehicles
        {
            get { return vehicles; }
            set { vehicles = value; }
        }


        public RepairShop(int capacity)
        {
            this.capacity = capacity;
            Vehicles=new List<Vehicle>();
        }



        public void AddVehicle(Vehicle vehicle)
        {
            if (Capacity>Vehicles.Count)
            {
                Vehicles.Add(vehicle);
            }
        }

        public bool RemoveVehicle(string vin)
        {
            if (Vehicles.Any(x=>x.VIN==vin))
            {
                Vehicles.Remove(Vehicles.FirstOrDefault(x => x.VIN == vin));
                return true;
            }
            return false;
        }

        public int GetCount()
        {
            return Vehicles.Count;
        }


        public Vehicle GetLowestMileage()
        {
            return Vehicles.OrderBy(x => x.Mileage).First();
        }

        public string Report()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("Vehicles in the preparatory:".TrimEnd());
            foreach (var vehicleee in Vehicles)
            {
                result.AppendLine(vehicleee.ToString().TrimEnd());
            }
            return result.ToString().TrimEnd();
        }
    }
}
