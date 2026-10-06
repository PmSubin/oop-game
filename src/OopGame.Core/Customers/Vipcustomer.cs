using OopGame.Core.Menu;

namespace OopGame.Core.Customers;

/// <summary>
/// Đại gia: tip rất đậm nhưng chỉ chờ được 25 giây.
/// </summary>
public class VipCustomer : Customer
{
    /// <summary>
    /// Tạo đại gia với kiên nhẫn tối đa 25 giây.
    /// </summary>
    public VipCustomer(string name, Dish order) : base(name, order, 25) { }

    /// <summary>
    /// Tên loại khách.
    /// </summary>
    public override string TypeName => "Đại gia";

    /// <summary>
    /// Chờ không quá 15 giây thì tip 30% giá món, ngược lại tip 10%.
    /// </summary>
    public override int CalculateTip()
    {
        if (WaitedSeconds <= 15)
        {
            return Order.Price * 30 / 100;
        }
        return Order.Price * 10 / 100;
    }

    /// <summary>
    /// Câu cảm ơn kiểu đại gia.
    /// </summary>
    public override string GetThankYouMessage()
    {
        return "Ngon! Khỏi thối tiền thừa.";
    }

    /// <summary>
    /// Câu nói khi đại gia bỏ về.
    /// </summary>
    public override string GetLeavingMessage()
    {
        return "Đại gia mà bắt chờ à? Không bao giờ quay lại!";
    }
}