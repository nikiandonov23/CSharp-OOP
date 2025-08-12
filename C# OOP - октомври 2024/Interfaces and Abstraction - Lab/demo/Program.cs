namespace demo
{
    public class Program
    {
        static void Main(string[] args)
        {
            Izpraven_chovek one=new Izpraven_chovek();
            one.MakeSound();
            one.Shit();


            Razumen_Chovek two = new Razumen_Chovek();
            two.MakeSound();
            two.Shit();

        }
    }
}
