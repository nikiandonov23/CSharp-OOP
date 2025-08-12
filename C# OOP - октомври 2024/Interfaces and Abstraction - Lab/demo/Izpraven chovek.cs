namespace demo;

public class Izpraven_chovek:Chovek
{
    public int Age { get; set; }
    public string EyesColour { get; set; }



    public  void MakeSound()
    {
        Console.WriteLine("UGA BUGAAA");
    }

    public  void Shit()
    {
        Console.WriteLine("PRUC Prrrrruc");
        ;
    }

    public Izpraven_chovek()
    {
    }
}