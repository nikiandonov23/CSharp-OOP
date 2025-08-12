using System.Text;

namespace AutomotiveRepairShop
{
    public class Vehicle
    {
        private string vin;

        public string VIN
        {
            get { return vin; }
            set { vin = value; }
        }


        private int mileage;

        public int Mileage
        {
            get { return mileage; }
            set { mileage = value; }
        }


        private string damage;

        public string Damage
        {
            get { return damage; }
            set { damage = value; }
        }


        public Vehicle(string vin, int mileage, string damage)
        {
            this.vin = vin;
            this.mileage = mileage;
            this.damage = damage;
        }

        public override string ToString()
        {
            return $"Damage: {damage}, Vehicle: {vin} ({mileage} km)";
        }


        
    }
}
