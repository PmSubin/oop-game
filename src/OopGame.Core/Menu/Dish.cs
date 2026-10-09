namespace OopGame.Core.Menu;

// lop cha truu tuong cho moi mon an trong quan
public abstract class Dish
{
    public string Name { get; }
    public int Price { get; }
    public int CookTimeSeconds { get; }

    protected Dish(string name, int price, int cookTimeSeconds)
    {
        if (price <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(price), "Giá món phải lớn hơn 0.");
        }
        if (cookTimeSeconds <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(cookTimeSeconds), "Thời gian nấu phải lớn hơn 0.");
        }

        Name = name;
        Price = price;
        CookTimeSeconds = cookTimeSeconds;
    }

    // nguyen lieu can de nau mot phan: ten nguyen lieu va so luong
    public abstract IReadOnlyDictionary<string, int> GetIngredients();

    // cau slogan vui cua mon, hien tren giao dien khi chon mon
    public abstract string Slogan { get; }

    // gia luon hien kieu viet nam (45.000d), khong phu thuoc cai dat ngon ngu cua may
    public override string ToString()
    {
        return $"{Name} - {Price.ToString("N0", VietnameseCulture)}đ";
    }

    private static readonly System.Globalization.CultureInfo VietnameseCulture =
        System.Globalization.CultureInfo.GetCultureInfo("vi-VN");
}
