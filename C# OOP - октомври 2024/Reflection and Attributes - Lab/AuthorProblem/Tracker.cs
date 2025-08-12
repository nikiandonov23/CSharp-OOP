using System.Reflection;

namespace AuthorProblem;

public class Tracker
{
     public void PrintMethodsByAuthor()
    {
        Type type=typeof(StartUp);

        MethodInfo[] methods=type.GetMethods();
        foreach (MethodInfo method in methods)
        {
         
            if (method.CustomAttributes.Any(x=>x.AttributeType==typeof(AuthorAttribute)))
            {
                var attributes = method.GetCustomAttributes(false);

                foreach (AuthorAttribute attr in attributes)
                {
                    Console.WriteLine($"{method.Name} is written by {attr.Name}");
                }



            }
        }
    }
}