using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClothesMagazine
{
    public class Magazine
    {
        private string type;
        public string Type
        {
            get { return type; }
            set { type = value; }
        }


        private int capacity;
        public int Capacity
        {
            get { return capacity; }
            set { capacity = value; }
        }


        private List<Cloth> clothes;
        public List<Cloth> Clothes
        {
            get { return clothes; }
            set { clothes = value; }
        }


        public Magazine(string type, int capacity)
        {
            this.type = type;
            this.capacity = capacity;
            Clothes=new List<Cloth>();
        }




        public void AddCloth(Cloth cloth)
        {
            if (Capacity>Clothes.Count)
            {
                Clothes.Add(cloth);
            }
        }


        public bool RemoveCloth(string color)
        {
            Cloth clothToBeRemoved = Clothes.FirstOrDefault(x => x.Color == color);
            if (clothToBeRemoved != null)
            {
                Clothes.Remove(clothToBeRemoved);
                return true;
            }
            return false;
        }


        public Cloth  GetSmallestCloth()
        {
            return Clothes.OrderBy(x => x.Size).First();
        }


        public Cloth GetCloth(string color)
        {
            return Clothes.FirstOrDefault(x => x.Color == color);
        }


        public int GetClothCount()
        {
            return Clothes.Count;
        }


        public string Report()
        {
           Clothes= Clothes.OrderBy(x => x.Size).ToList();
            StringBuilder result=new StringBuilder();
            result.AppendLine($"{type} magazine contains:");

            foreach (var cloth in Clothes)
            {
                result.AppendLine($"{cloth}");
            }
            return result.ToString().Trim();
        }
    }
}
