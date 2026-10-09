using OopGame.Core.Menu;

namespace OopGame.Core.Customers;

// khach vang lai: de tinh, cho duoc 40 giay
public class NormalCustomer : Customer
{
    // tao khach vang lai voi kien nhan toi da 40 giay
    public NormalCustomer(string name, Dish order) : base(name, order, 40) { }

    // ten loai khach
    public override string TypeName => "Khách vãng lai";

    // cho khong qua 20 giay thi tip 10% gia mon, nguoc lai khong tip
    public override int CalculateTip()
    {
        if (WaitedSeconds <= 20)
        {
            return Order.Price * 10 / 100;
        }
        return 0;
    }

    // cau cam on cua khach vang lai
    public override string GetThankYouMessage()
    {
        return "Ngon, cảm ơn quán nha!";
    }

    // cau noi khi khach vang lai bo ve
    public override string GetLeavingMessage()
    {
        return "Thôi đi quán khác vậy...";
    }
}