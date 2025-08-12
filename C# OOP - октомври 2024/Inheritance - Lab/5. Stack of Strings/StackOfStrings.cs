namespace CustomStack;

public class StackOfStrings : Stack<string>
{




    public bool IsEmpty()
    {

        if (this.Count == 0) return true;
        return false;
    }


    public void AddRange(List<string> elements)
    {
        foreach (var item in elements)
        {
            this.Push(item);
        }
    }

}