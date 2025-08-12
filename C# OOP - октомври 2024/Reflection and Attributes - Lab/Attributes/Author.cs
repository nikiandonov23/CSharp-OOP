namespace Attributes;

public class Author:Attribute
{
    public string firstName { get; set; }
    public string lastName { get; set; }

    public Author(string firstName, string lastName)
    {
        this.firstName = firstName;
        this.lastName = lastName;
    }
}