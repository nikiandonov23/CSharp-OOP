namespace _5._Play_Catch
{
    public class Program
    {
        public static void Main()
        {

            List<int> input = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

            int exeptions = 0;


            while (exeptions < 3)
            {
                string[] tockens = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string action = tockens[0];



                try
                {

                    switch (action)
                    {
                        case "Replace":
                            int index = int.Parse(tockens[1]);
                            int element = int.Parse(tockens[2]);

                            input.Insert(index, element);
                            input.RemoveAt(index + 1);
                            break;

                        case "Print":
                            int startIndex = int.Parse(tockens[1]);
                            int endIndex = int.Parse(tockens[2]);

                            List<int> toBePrinted = input.GetRange(startIndex, endIndex);
                            Console.WriteLine(string.Join(", ", toBePrinted));
                            break;

                        case "Show":
                            index = int.Parse(tockens[1]);
                            Console.WriteLine(input[index]);
                            break;

                    }
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine("The index does not exist!");
                    exeptions++;
                }
                catch (FormatException e)
                {
                    Console.WriteLine("The variable is not in the correct format!");
                    exeptions++;
                }
            }

            Console.WriteLine(string.Join(", ",input));



        }
    }
}
