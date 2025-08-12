namespace Telephony;

public class Smartphone:ICaller,IBrowser
{
    public void CallOrDail(string number)
    {
        Console.WriteLine($"Calling... {number}");
    }

    public void Browse(string url)
    {
        Console.WriteLine($"Browsing: {url}!");
    }
}