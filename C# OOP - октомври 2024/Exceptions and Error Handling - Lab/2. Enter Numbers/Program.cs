namespace _2._Enter_Numbers
{
    public class Program
    {
        public static void Main()
        {
            int start = 1;
            int end = 100;

            List<int>allNumbers=new List<int>();
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    int currentNumber = ReadNumber(start, end);
                    allNumbers.Add(currentNumber);
                    start = allNumbers.Last();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    i--;

                }

            }

            Console.WriteLine(string.Join(", ",allNumbers));


            int ReadNumber(int start, int end)
            {
                string input = Console.ReadLine();


                if (!int.TryParse(input, out int result))

                    throw new Exception("Invalid Number!");
                


                if (result <= start || result >= end)
                
                    throw new Exception($"Your number is not in range {start} - 100!");
                

                return result;

            }


         









        }


    }
}
