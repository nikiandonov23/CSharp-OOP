namespace ClothesMagazine
{
    public class Cloth
    {
		private string color;
		public string Color
        {
			get { return color; }
			set { color = value; }
		}

		private int size;
		public int Size
        {
			get { return size; }
			set { size = value; }
		}

		private string type;
		public string Type
        {
			get { return type; }
			set { type = value; }
		}

        public Cloth(string color, int size, string type)
        {
            this.color = color;
            this.size = size;
            this.type = type;
        }


        public override string ToString()
        {
            return $"Product: {type} with size {size}, color {color}";
        }
    }
}
