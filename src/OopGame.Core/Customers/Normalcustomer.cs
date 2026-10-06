using OopGame.Core.Menu;

namespace OopGame.Core.Customers;

/// <summary>
/// Khách vãng lai: dễ tính, chờ được 40 giây.
/// </summary>
public class NormalCustomer : Customer
{
    /// <summary>
    /// Tạo khách vãng lai với kiên nhẫn tối đa 40 giây.
    /// </summary>
    public NormalCustomer(string name, Dish order) : base(name, order, 40) { }

    /// <summary>
    /// Tên loại khách.
    /// </summary>
    public override string TypeName => "Khách vãng lai";

    /// <summary>
    /// Chờ không quá 20 giây thì tip 10% giá món, ngược lại không tip.
    /// </summary>
    public override int CalculateTip()
    {
        if (WaitedSeconds <= 20)
        {
            return Order.Price * 10 / 100;
        }
        return 0;
    }

    /// <summary>
    /// Câu cảm ơn của khách vãng lai.
    /// </summary>
    public override string GetThankYouMessage()
    {
        return "Ngon, cảm ơn quán nha!";
    }

    /// <summary>
    /// Câu nói khi khách vãng lai bỏ về.
    /// </summary>
    public override string GetLeavingMessage()
    {
        return "Thôi đi quán khác vậy...";
    }
}