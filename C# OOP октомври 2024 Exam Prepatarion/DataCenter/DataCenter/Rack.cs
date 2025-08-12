using System.Text;

namespace DataCenter
{
    public class Rack
    {
        private int slots;

        public int Slots
        {
            get { return slots; }
            set { slots = value; }
        }
        private List<Server> servers;

        public List<Server> Servers
        {
            get { return servers; }
            set { servers = value; }
        }


        private int getCount;

        public int GetCount
        {
            get { return Servers.Count; }

        }

        public Rack(int slots)
        {
            Servers = new List<Server>();
            this.slots = slots;
        }


        public void AddServer(Server server)
        {

            if (!Servers.Any(x=>x.SerialNumber==server.SerialNumber) && slots>Servers.Count)
            {
                Servers.Add(server);
            }
        }
    

        public bool RemoveServer(string serialNumber)
        {
            if (Servers.Any(x => x.SerialNumber == serialNumber))
            {
                Servers.Remove(Servers.FirstOrDefault(x => x.SerialNumber == serialNumber));
                return true;
            }
            else
            {
                return false;
            }
        }


        public string GetHighestPowerUsage()   //organiziram gi po red i vrushtam nai golqmoto
        {
            Server serverToBeReturned = Servers.OrderByDescending(x => x.PowerUsage).First();

            return serverToBeReturned.ToString();
        }

        public int GetTotalCapacity()
        {
            int totalCapacity = 0;
            foreach (var server in Servers)
            {
                totalCapacity += server.Capacity;
            }
            return totalCapacity;
        }


        public string DeviceManager()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine($"{Servers.Count} servers operating:");
            foreach (var server in Servers)
            {
                result.AppendLine($"{server.ToString()}");
            }
            return result.ToString().Trim();
        }


    }
}
