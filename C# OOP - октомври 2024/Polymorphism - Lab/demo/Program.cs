using System.Security.Cryptography.X509Certificates;

namespace demo
{
    public class Program
    {
        public static void Main()
        {
            IMusic mangala = new Mangal();
            IMusic hiphopara = new Hiphopara();


            Sing(mangala);
            Sing(hiphopara);



            WriteLine(5);
            WriteLine(DateTime.Now);


            void WriteLine(Object obj)
            {
                Console.WriteLine(obj.ToString());
                
                ;
            }







            void Sing(IMusic subject)
            {
                subject.sing();
            }

        }

    }
}
