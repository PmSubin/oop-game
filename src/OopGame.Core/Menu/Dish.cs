namespace OopGame.Core.Menu;

/// <summary>
/// Lớp cha trừu tượng cho mọi món ăn trong quán.
/// Không tạo trực tiếp được, phải tạo qua lớp con (Pho, BanhMi, ComTam...).
/// </summary>
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

    /// <summary>
    /// Nguyên liệu cần để nấu một phần: tên nguyên liệu và số lượng.
    /// Mỗi món con tự khai báo công thức của mình.
    /// </summary>
    public abstract IReadOnlyDictionary<string, int> GetIngredients();

    /// <summary>
    /// Câu slogan vui của món, hiện trên giao diện khi chọn món.
    /// Mỗi món con tự viết câu của mình.
    /// </summary>
    public abstract string Slogan { get; }

    // Giá luôn hiện kiểu Việt Nam (45.000đ), không phụ thuộc cài đặt ngôn ngữ của máy.
    public override string ToString()
    {
        return $"{Name} - {Price.ToString("N0", VietnameseCulture)}đ";
    }

    private static readonly System.Globalization.CultureInfo VietnameseCulture =
        System.Globalization.CultureInfo.GetCultureInfo("vi-VN");
}
