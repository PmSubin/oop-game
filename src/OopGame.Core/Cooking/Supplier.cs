namespace OopGame.Core.Cooking;

// nha cung cap: giu bang gia mua tung nguyen lieu
public class Supplier
{
    private readonly Dictionary<string, int> _prices = new Dictionary<string, int>(); // gia 1 phan, don vi dong

    // tao nha cung cap voi bang gia cua 13 nguyen lieu
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

    // gia mua mot phan nguyen lieu
    public int GetPrice(string ingredient)
    {
        int price;
        if (_prices.TryGetValue(ingredient, out price))
        {
            return price;
        }
        throw new ArgumentException("Nhà cung cấp không bán nguyên liệu này.", nameof(ingredient));
    }

    // tong tien mua mot so luong nguyen lieu (gia mot phan nhan so luong)
    public int GetTotalPrice(string ingredient, int amount)
    {
        return GetPrice(ingredient) * amount;
    }

    // danh sach ten cac nguyen lieu nha cung cap co ban, dang chi doc
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