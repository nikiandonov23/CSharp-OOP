using System.ComponentModel;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Xml.Linq;

namespace Stealer;

public class Spy
{
    public string StealFieldInfo(string nameOfTheClass, string[] fieldAndNames)
    {
        StringBuilder result = new StringBuilder();

        Type type = Type.GetType(nameOfTheClass);  //получаване на типа на класа
        result.AppendLine($"Class under investigation: {nameOfTheClass}");



        Object classInstance = Activator.CreateInstance(type);

        foreach (var field in fieldAndNames)
        {

            FieldInfo currrentField = type.GetField(field, (BindingFlags)60);
            if (currrentField != null)
            {
                Object value = currrentField.GetValue(classInstance);
                result.AppendLine($"{currrentField.Name} = {value}");

            }

        }


        return result.ToString().Trim();
    }

    public string AnalyzeAccessModifiers(string className)
    {
        StringBuilder result = new StringBuilder();
        Type type = Type.GetType(className);



        FieldInfo[] publicFields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static);
        foreach (var publicField in publicFields)
        {
            result.AppendLine($"{publicField.Name} must be private!");
        }



        MethodInfo[] privateMethods = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);
        foreach (var privateMethod in privateMethods.Where(x => x.Name.StartsWith("get")))
        {
            result.AppendLine($"{privateMethod.Name} have to be private!");
        }




        MethodInfo[] publicMethods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static);
        foreach (var publicMethod in publicMethods.Where(x => x.Name.StartsWith("set")))
        {
            result.AppendLine($"{publicMethod.Name} have to be private!");
        }






        return result.ToString().Trim();
    }

    public string RevealPrivateMethods(string className)
    {
        StringBuilder result = new StringBuilder();
        Type type = Type.GetType(className);

        result.AppendLine($"All Private Methods of Class: {type.FullName}");
        result.AppendLine($"Base Class: {type.BaseType.Name}");

        MethodInfo[] privateMethods = type.GetMethods(BindingFlags.NonPublic|BindingFlags.Instance);

        foreach (var privateMethod in privateMethods)
        {
            result.AppendLine($"{privateMethod.Name}");
        }






        return result.ToString().Trim();


    }

    public string CollectAllGettersAndSetters(string className)
    {
        StringBuilder result = new StringBuilder();
        Type type=Type.GetType(className);

        MethodInfo[] allMethods = type.GetMethods((BindingFlags)60);

        foreach (var method in allMethods.Where(x=>x.Name.StartsWith("get")))
        {
            result.AppendLine($"{method.Name} will return {method.ReturnType}");
        }

        foreach (var method in allMethods.Where(x=>x.Name.StartsWith("set")))
        {
            result.AppendLine($"{method.Name} will set field of {method.GetParameters().First().ParameterType}");
        }










        return result.ToString().Trim();
    }
}