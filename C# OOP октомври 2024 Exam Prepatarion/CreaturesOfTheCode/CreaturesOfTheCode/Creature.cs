using System.Text;

namespace CreaturesOfTheCode
{
    public class Creature
    {
		private string name;

		public string Name
		{
			get { return name; }
			set { name = value; }
		}


		private string kind;

		public string Kind
		{
			get { return kind; }
			set { kind = value; }
		}


		private int health;

		public int 	Health 
		{
			get { return health; }
			set { health = value; }
		}



		private List<string> abilities;

        public List<string> Abilities
        {
			get { return abilities; }
			set { abilities = value; }
		}


        public Creature(string name, string kind, int health, string abilities)
        {
            this.Name = name;
            this.Kind = kind;
            this.Health = health;
            this.Abilities = abilities.Split(", ").ToList();
        }

        public override string ToString()
        {
			StringBuilder result=new StringBuilder();
            result.AppendLine($"{Name} ({Kind}) has {Health} HP");
            result.AppendLine($"Abilities: {string.Join(", ",Abilities)}");
            return result.ToString().Trim();
        }
    }
}
