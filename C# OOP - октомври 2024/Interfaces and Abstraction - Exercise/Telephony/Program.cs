namespace Telephony
{
    public class Program
    {
        static void Main()
        {
            List<string> numbers = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();

            List<string> urls = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();

            foreach (var number in numbers)
            {
                bool isValid = true;
                foreach (var character in number)
                {
                   

                    if (!char.IsDigit(character))
                    {
                        Console.WriteLine($"Invalid number!");
                        isValid = false;
                        break;
                    }
                }

                if (number.Length == 10 && isValid )
                {
                    Smartphone smartphone = new Smartphone();
                    smartphone.CallOrDail(number);

                }

                else if (number.Length == 7&&isValid)
                {
                    StationaryPhone stationaryPhone = new StationaryPhone();
                    stationaryPhone.CallOrDail(number);

                }
            }

            foreach (var url in urls)
            {
                bool isValid = true;
                foreach (var character in url)
                {
                    if (char.IsDigit(character))
                    {
                        Console.WriteLine("Invalid URL!");
                        isValid = false;
                        break;
                    }
                }

                if (isValid)
                {
                    Smartphone smartphone=new Smartphone();
                    smartphone.Browse(url);
                }
            }




        }
    }
}
