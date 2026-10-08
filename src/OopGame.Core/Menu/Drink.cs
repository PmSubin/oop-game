namespace OopGame.Core.Menu;

/// <summary>
/// Lớp cha cho đồ uống.
/// </summary>
public abstract class Drink : Dish
{
    /// <summary>
    /// Khởi tạo đồ uống.
    /// </summary>
    protected Drink(string name, int price, int cookTimeSeconds)
        : base(name, price, cookTimeSeconds)
    {
    }

    /// <summary>
    /// Hiển thị tên đồ uống.
    /// </summary>
    public override string ToString()
    {
        return "[Đồ uống] " + base.ToString();
    }
}