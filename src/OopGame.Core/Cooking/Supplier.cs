namespace OopGame.Core.Cooking;

/// <summary>
/// Nhà cung cấp: giữ bảng giá mua từng nguyên liệu.
/// </summary>
public class Supplier
{
    private readonly Dictionary<string, int> _prices = new Dictionary<string, int>();   // giá 1 phần, đơn vị đồng

    /// <summary>
    /// Tạo nhà cung cấp với bảng giá của 13 nguyên liệu.
    /// </summary>
    public Supplier()
    {
        _prices.Add(Ingredients.RiceNoodle, 5000);
        _prices.Add(Ingredients.Vermicelli, 5000);
        _prices.Add(Ingredients.Beef, 15000);
        _prices.Add(Ingredients.Pork, 10000);
        _prices.Add(Ingredients.Rice, 3000);
        _prices.Add(Ingredients.Bread, 4000);
        _prices.Add(Ingredients.Egg, 3000);
        _prices.Add(Ingredients.Vegetables, 2000);
        _prices.Add(Ingredients.Tea, 1000);
        _prices.Add(Ingredients.Ice, 500);
        _prices.Add(Ingredients.InstantNoodle, 4000);
        _prices.Add(Ingredients.Milk, 5000);
        _prices.Add(Ingredients.Pearl, 6000);
    }

    /// <summary>
    /// Giá mua một phần nguyên liệu. Nguyên liệu không có trong bảng giá thì ném ArgumentException.
    /// </summary>
    public int GetPrice(string ingredient)
    {
        int price;
        if (_prices.TryGetValue(ingredient, out price))
        {
            return price;
        }
        throw new ArgumentException("Nhà cung cấp không bán nguyên liệu này.", nameof(ingredient));
    }

    /// <summary>
    /// Tổng tiền mua một số lượng nguyên liệu (giá một phần nhân số lượng).
    /// </summary>
    public int GetTotalPrice(string ingredient, int amount)
    {
        return GetPrice(ingredient) * amount;
    }

    /// <summary>
    /// Danh sách tên các nguyên liệu nhà cung cấp có bán, dạng chỉ đọc.
    /// </summary>
    public IReadOnlyList<string> GetAvailableIngredients()
    {
        List<string> names = new List<string>();
        foreach (string name in _prices.Keys)
        {
            names.Add(name);
        }
        return names.AsReadOnly();
    }
}