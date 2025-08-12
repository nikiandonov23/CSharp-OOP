namespace Raiding
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<BaseHero> allHeroes=new List<BaseHero>();

            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                string name=Console.ReadLine();
                string type=Console.ReadLine();
                BaseHero currenHero;
                switch (type)
                {
                    

                    case "Druid":
                         currenHero = new Druid(name);
                        allHeroes.Add(currenHero);
                        break;

                    case "Paladin":
                         currenHero = new Paladin(name);
                        allHeroes.Add(currenHero);
                        break;


                    case "Rogue":
                        currenHero = new Rogue(name);
                        allHeroes.Add(currenHero);
                        break;

                    case "Warrior":
                        currenHero = new Warrior(name);
                        allHeroes.Add(currenHero);
                        break;

                    default:
                        Console.WriteLine("Invalid hero!");
                        i--;
                        break;
                }
            }

            int bossPower=int.Parse(Console.ReadLine());
            int allPower = 0;


            foreach (var hero in allHeroes)
            {
               
                if (hero.GetType().Name=="Druid")
                {
                    allPower += 80;
                }
                else if (hero.GetType().Name == "Paladin")
                {
                    allPower += 100;
                }
                else if (hero.GetType().Name == "Rogue")
                {
                    allPower += 80;
                }
                else if (hero.GetType().Name == "Warrior")
                {
                    allPower += 100;
                }

                Console.WriteLine(hero.CastAbility());

            }

            if (allPower>=bossPower)
            {
                Console.WriteLine("Victory!");
            }
            else
            {
                Console.WriteLine("Defeat...");
            }


        }
    }
}
