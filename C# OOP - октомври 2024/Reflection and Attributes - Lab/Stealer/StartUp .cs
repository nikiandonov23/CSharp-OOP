using System.Reflection;

namespace Stealer
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            Spy spy=new Spy();

            string result = spy.CollectAllGettersAndSetters("Stealer.Hacker");
            Console.WriteLine(result);

            


        }
    }
}
