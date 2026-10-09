using OopGame.Core.Menu;

namespace OopGame.Core.Customers;

// "may de khach": tao khach ngau nhien voi ten vui, mon ngau nhien va ti le 60% vang lai, 20% dai gia, 20% reviewer
public class CustomerFactory
{
    private readonly Random _random;

    private static readonly string[] _names = new string[]
    {
        "Anh Shipper Vội Vàng", "Chị Review Một Sao", "Em Sinh Viên Cuối Tháng", "Anh Code Dạo",
        "Chị Bán Hàng Online", "Bác Bảo Vệ Trường", "Anh Gym Ăn Kiêng", "Cô Hàng Xóm Hóng Chuyện",
        "Em Fan Cứng Trà Sữa", "Bạn Trưởng Nhóm Chạy Deadline", "Chú Xe Ôm Hay Kể Chuyện"
    };

    // tao may de khach, moi lua chon ngau nhien deu dung Random duoc truyen vao
    public CustomerFactory(Random random)
    {
        _random = random;
    }

    // tao mot khach ngau nhien, goi mot mon ngau nhien trong thuc don
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