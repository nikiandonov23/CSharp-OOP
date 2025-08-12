using System.Security.Cryptography;

namespace CustomStack;

public class RandomList:List<string>
{
   



    public string RandomString()
    {
        Random rnd = new Random();
        int randomNumber = rnd.Next(0, this.Count);

        var toBeRemoved = this[randomNumber];

        this.Remove(toBeRemoved);

        return toBeRemoved;


    }
}