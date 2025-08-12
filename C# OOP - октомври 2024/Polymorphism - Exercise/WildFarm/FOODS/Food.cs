namespace WildFarm.FOODS;

public abstract class Food
{
    public int Quantity { get; }

    protected Food(int quantity)
    {
        Quantity = quantity;
    }


}