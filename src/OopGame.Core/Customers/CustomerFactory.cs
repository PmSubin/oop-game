using OopGame.Core.Menu;

namespace OopGame.Core.Customers;

/// <summary>
/// "Máy đẻ khách": tạo khách ngẫu nhiên với tên vui, món ngẫu nhiên và tỉ lệ 60% vãng lai, 20% đại gia, 20% reviewer.
/// </summary>
public class CustomerFactory
{
    private readonly Random _random;

    private static readonly string[] _names = new string[]
    {
        "Anh Shipper Vội Vàng", "Chị Review Một Sao", "Em Sinh Viên Cuối Tháng", "Anh Code Dạo",
        "Chị Bán Hàng Online", "Bác Bảo Vệ Trường", "Anh Gym Ăn Kiêng", "Cô Hàng Xóm Hóng Chuyện",
        "Em Fan Cứng Trà Sữa", "Bạn Trưởng Nhóm Chạy Deadline", "Chú Xe Ôm Hay Kể Chuyện"
    };

    /// <summary>
    /// Tạo máy đẻ khách, mọi lựa chọn ngẫu nhiên đều dùng Random được truyền vào.
    /// </summary>
    public CustomerFactory(Random random)
    {
        _random = random;
    }

    /// <summary>
    /// Tạo một khách ngẫu nhiên, gọi một món ngẫu nhiên trong thực đơn.
    /// Ném ArgumentException nếu thực đơn trống.
    /// </summary>
    public Customer CreateRandom(IReadOnlyList<Dish> menu)
    {
        if (menu.Count == 0)
        {
            throw new ArgumentException("Thực đơn đang trống.", nameof(menu));
        }

        Dish order = menu[_random.Next(menu.Count)];
        string name = _names[_random.Next(_names.Length)];
        int roll = _random.Next(100);

        if (roll < 60)
        {
            return new NormalCustomer(name, order);
        }
        if (roll < 80)
        {
            return new VipCustomer(name, order);
        }
        return new PickyCustomer(name, order);
    }
}