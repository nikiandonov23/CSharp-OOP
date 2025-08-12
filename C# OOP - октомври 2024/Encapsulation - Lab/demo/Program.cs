namespace demo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            

            Worker worker=new Worker();
            worker.Salary = 660;
            Console.WriteLine(worker.Salary);

            Console.WriteLine(worker.Name);
        }
    }
}
