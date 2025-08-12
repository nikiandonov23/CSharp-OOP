using System.Text;

namespace CreaturesOfTheCode
{
    public class MythicalCreaturesHub
    {

        private List<Creature> creatures;
        public List<Creature> Creatures
        {
            get { return creatures; }
            set { creatures = value; }
        }


        private int capacity;
        public int Capacity
        {
            get { return capacity; }
            set { capacity = value; }
        }


        public MythicalCreaturesHub(int capacity)
        {
            this.Capacity = capacity;
            Creatures = new List<Creature>();
        }



        public void AddCreature(Creature creature)    //uj ba4ka no pak da go proverq 4e me sumnqva
        {
            if (Capacity > Creatures.Count)
            {
                if (!Creatures.Any(x => x.Name.ToLower() == creature.Name.ToLower()))
                {
                    Creatures.Add(creature);
                }

            }

        }

        public bool RemoveCreature(string name)
        {
            var creatureToBeRemoved = Creatures.FirstOrDefault(x => x.Name == name);
            if (creatureToBeRemoved != null)
            {
                Creatures.Remove(creatureToBeRemoved);
                return true;
            }
            return false;
        }

        public Creature GetStrongestCreature()
        {

            var toBeReturned = Creatures.OrderByDescending(x => x.Health).First();
            return toBeReturned;

        }

        public string Details(string creatureName)
        {
            var creatureToBeReturned = Creatures.FirstOrDefault(x => x.Name == creatureName);

            if (creatureToBeReturned != null)
            {
                return creatureToBeReturned.ToString().Trim();
            }
            else
            {
                return $"Creature with the name {creatureName} not found.";
            }
        }


        public string GetAllCreatures()
        {
            var sortedCreatures = Creatures.OrderBy(x => x.Name);

            StringBuilder result=new StringBuilder();
            result.AppendLine("Mythical Creatures:");
            foreach (var creature in sortedCreatures)
            {
                result.AppendLine($"{creature.Name} -> { creature.Kind}");
            }
            return result.ToString().Trim();
        }
    }
}
