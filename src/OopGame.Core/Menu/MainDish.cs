namespace OopGame.Core.Menu;

/// <summary>
/// Lớp cha cho các món chính.
/// </summary>
public abstract class MainDish : Dish
{
    /// <summary>
    /// Khởi tạo món chính.
    /// </summary>
    protected MainDish(string name, int price, int cookTimeSeconds)
        : base(name, price, cookTimeSeconds)
    {
    }

    /// <summary>
    /// Hiển thị tên món chính.
    /// </summary>
    public override string ToString()
    {
        return "[Món chính] " + base.ToString();
    }
}