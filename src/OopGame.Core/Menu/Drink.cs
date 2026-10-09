namespace OopGame.Core.Menu;

// lop cha cho do uong
public abstract class Drink : Dish
{
    // khoi tao do uong
    protected Drink(string name, int price, int cookTimeSeconds)
        : base(name, price, cookTimeSeconds)
    {
    }

    // hien thi ten do uong
    public override string ToString()
    {
        return "[Đồ uống] " + base.ToString();
    }
}