using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace DataCenter
{
    public class Server
    {
        private string serialNumber;
        public string SerialNumber
        {
            get { return serialNumber; }
            set { serialNumber = value; }
        }

        private string model;
        public string Model
        {
            get { return model; }
            set { model = value; }
        }


        private int capacity;
        public int Capacity
        {
            get { return capacity; }
            set { capacity = value; }
        }

        private int powerUsage;
        public int PowerUsage
        {
            get { return powerUsage; }
            set { powerUsage = value; }
        }


        public Server(string serialNumber, string model, int capacity, int powerUsage)
        {
            this.serialNumber = serialNumber;
            this.model = model;
            this.capacity = capacity;
            this.powerUsage = powerUsage;
        }

        public override string ToString()
        {
            StringBuilder result=new StringBuilder();

            result.Append($"Server {SerialNumber}: {Model}, {Capacity}TB, {PowerUsage}W");
            return result.ToString().Trim();

        }
    }
}
