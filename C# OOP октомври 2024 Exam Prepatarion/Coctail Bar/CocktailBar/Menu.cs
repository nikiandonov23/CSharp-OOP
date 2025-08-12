using System.Text;

namespace CocktailBar
{
    public class Menu
    {
        private List<Cocktail> cocktails;

        public List<Cocktail> Cocktails
        {
            get { return cocktails; }
            set { cocktails = value; }
        }



        private int barCapacity;
        public int BarCapacity
        {
            get { return barCapacity; }
            set { barCapacity = value; }
        }




        public Menu(int barCapacity)    //ctor
        {
            this.barCapacity = barCapacity;
            Cocktails=new List<Cocktail>();
        }


        public void AddCocktail(Cocktail cocktail)  //OK
        {
            if (barCapacity>Cocktails.Count)
            {
                if (!Cocktails.Any(x=>x.Name==cocktail.Name))  //ako ne sudurja imeto
                {
                    Cocktails.Add(cocktail);
                }
            }
        }



        public bool RemoveCocktail(string name)   // OK beeee ba4ka
        {
            var coctailToBeRemoved = Cocktails.FirstOrDefault(x => x.Name == name);
            if (coctailToBeRemoved!=null)
            {
                Cocktails.Remove(coctailToBeRemoved);
                return true;
            }
            else
            {
                return false;
            }

        }


        public Cocktail GetMostDiverse()  // narejdam gi descending i vrushtam samo purviq koito se qvqva i s nai mnogo ingridients
        {
            if (Cocktails.Count!=0)
            {
                Cocktail coctailToBeReturned = Cocktails.OrderByDescending(x => x.Ingredients.Count).First();
                return coctailToBeReturned;
            }
            else
            {
                return null;
            }
           
        }


        public string Details(string cocktailName)
        {
            var coctailToBeReturned = Cocktails.FirstOrDefault(name => name.Name == cocktailName);
          

            return coctailToBeReturned.ToString();
        }



        public string GetAll()
        {
            StringBuilder result = new StringBuilder();
            Cocktails=Cocktails.OrderBy(x =>x.Name).ToList();
            result.AppendLine($"All Cocktails:");
            foreach (var coctail in Cocktails)
            {
                result.AppendLine(coctail.Name);
            }
            return  result.ToString().Trim();
        }
    }
}
