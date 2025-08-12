using System.Numerics;
using System.Xml.Linq;

namespace _4._Sum_of_Integers
{
    public class Program
    {
        public static void Main()
        {

            int sum = 0;
            List<string> input = Console.ReadLine().Split(" ").ToList();



            foreach (var element in input)
            {
                try
                {
                    int currentNum = int.Parse(element);
                    sum += currentNum;
                }
                catch (FormatException e)
                {
                    Console.WriteLine($"The element '{element}' is in wrong format!");
                }
                catch (OverflowException e)
                {
                    Console.WriteLine($"The element '{element}' is out of range!");
                }

                finally
                {
                    Console.WriteLine($"Element '{element}' processed - current sum: {sum}");
                }
            }

            Console.WriteLine($"The total sum of all integers is: {sum}");



     









        }
    }
}
