using OopGame.Core.Menu;

namespace OopGame.Core.Customers;

// dai gia: tip rat dam nhung chi cho duoc 25 giay
public class VipCustomer : Customer
{
    // tao dai gia voi kien nhan toi da 25 giay
    public VipCustomer(string name, Dish order) : base(name, order, 25) { }

    // ten loai khach
    public override string TypeName => "Đại gia";

    // cho khong qua 15 giay thi tip 30% gia mon, nguoc lai tip 10%
    public override int CalculateTip()
    {
        if (WaitedSeconds <= 15)
        {
            return Order.Price * 30 / 100;
        }
        return Order.Price * 10 / 100;
    }

    // cau cam on kieu dai gia
    public override string GetThankYouMessage()
    {
        return "Ngon! Khỏi thối tiền thừa.";
    }

    // cau noi khi dai gia bo ve
    public override string GetLeavingMessage()
    {
        return "Đại gia mà bắt chờ à? Không bao giờ quay lại!";
    }
}