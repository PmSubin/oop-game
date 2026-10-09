namespace OopGame.Core.Menu;

// lop cha cho cac mon chinh
public abstract class MainDish : Dish
{
    // khoi tao mon chinh
    protected MainDish(string name, int price, int cookTimeSeconds)
        : base(name, price, cookTimeSeconds)
    {
    }

    // hien thi ten mon chinh
    public override string ToString()
    {
        return "[Món chính] " + base.ToString();
    }
}