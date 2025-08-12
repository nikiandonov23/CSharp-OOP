namespace Telephony;

public class StationaryPhone:ICaller
{
    public void CallOrDail(string number)
    {
        Console.WriteLine($"Dialing... {number}");
    }
}