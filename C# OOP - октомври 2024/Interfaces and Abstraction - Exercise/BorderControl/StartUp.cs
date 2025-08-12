using System.Security.Cryptography.X509Certificates;

namespace BorderControl
{
    public class StartUp
    {
        static void Main()
        {
            List<INamable> allSubjects = new List<INamable>();

            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                string[] tockens=Console.ReadLine().Split(" ",StringSplitOptions.RemoveEmptyEntries);
                string subject = tockens[0];


                if (tockens.Length==4)
                {
                    Citizen citizen = new Citizen(tockens[0], int.Parse(tockens[1]), tockens[2], tockens[3]);
                    allSubjects.Add(citizen);
                }
                else if (tockens.Length==3)
                {
                    Rebel rebel = new Rebel(tockens[0], int.Parse(tockens[1]), tockens[2]);
                    allSubjects.Add(rebel);
                }
            }

            string command = "";
            while ((command = Console.ReadLine()) != "End")
            {

                foreach (var subject in allSubjects)
                {
                    if (subject.Name==command)
                    {
                        subject.BuyFood();
                    }
                }

            }

            int totalFood=0;

            foreach (var subjects in allSubjects)
            {
                totalFood += subjects.Food;
            }

            Console.WriteLine(totalFood);



            
        }
    }
}
