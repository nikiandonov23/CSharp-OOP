namespace _01.Square_Root
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                int n = int.Parse(Console.ReadLine());
                if (n < 0)
                {
                    throw new Exception();
                }
                double result = Math.Sqrt(n);
                Console.WriteLine(result);
            }
            catch (Exception e)
            {
                Console.WriteLine("Invalid number.");

            }
            finally
            {
                Console.WriteLine("Goodbye.");
            }

        }
    }
}
