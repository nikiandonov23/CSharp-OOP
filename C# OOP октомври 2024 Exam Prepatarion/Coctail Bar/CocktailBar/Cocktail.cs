using System.Text;

namespace CocktailBar
{
    public class Cocktail
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private decimal price;

        public decimal Price
        {
            get { return price; }
            set { price = value; }
        }

        private double volume;

        public double Volume
        {
            get { return volume; }
            set { volume = value; }
        }




        private List<string> _ingredients;
        public Cocktail(string ingredients)
        {
            _ingredients = ingredients.Split(", ").ToList();
        }
        public List<string> Ingredients { get => _ingredients; }


        public Cocktail(string name, decimal price, double volume, string ingredients) :this( ingredients)
        {
            this.name = name;
            this.price = price;
            this.volume = volume;
            
        }

        public override string ToString()
        {
            StringBuilder result=new StringBuilder();
            result.AppendLine($"{Name}, Price: {Price:F2} BGN, Volume: {Volume} ml");
            result.AppendLine($"Ingredients: {string.Join(", ",Ingredients)}");
            return result.ToString().Trim();
        }


       
    }
}
