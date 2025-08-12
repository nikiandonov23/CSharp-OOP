namespace Card
{
    public class Program
    {
        public static void Main()
        {

            List<Card> cards = new List<Card>();
            

            Queue<string> input = new Queue<string>(Console.ReadLine()
                .Split(", "));
            while (input.Count>0)
            {
                string tockens = input.Dequeue();
                string[] tockensTwo=tockens.Split(" ");
                string face = tockensTwo[0];
                string suite = tockensTwo[1];

                try
                {
                    Card currentCard = CreateCard(face, suite);
                    cards.Add(currentCard);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                   
                }
            }

            Console.WriteLine(string.Join(" ",cards));



            Card CreateCard(string face, string suit)
            {
               
                
                    Card card = new Card(face, suit);
                    return card;
                
                
               
                
            }
        }

    }


    public class Card
    {
        public string Face { get; set; }
        public string Suit { get; set; }


        public override string ToString()
        {
            return $"[{this.Face}{this.Suit}]";
        }

        public Card(string face, string suit)
        {
            List<string> validFaces = new List<string>()
            {
                "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A"

            };

            if (validFaces.Contains(face))
            {
                Face = face;
            }
            else
            {
                throw new Exception("Invalid card!");

            }

            switch (suit)
            {
                case "S":
                    Suit = "\u2660";
                    break;

                case "H":
                    Suit = "\u2665";
                    break;

                case "D":
                    Suit = "\u2666";
                    break;

                case "C":
                    Suit = "\u2663";
                    break;
                default:
                    throw new Exception("Invalid card!");
                    break;
            }


        }
    }
}
