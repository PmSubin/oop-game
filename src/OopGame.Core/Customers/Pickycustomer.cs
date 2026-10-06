using OopGame.Core.Menu;

namespace OopGame.Core.Customers;

/// <summary>
/// Reviewer khó tính: chờ quá 10 giây là bực gấp đôi.
/// </summary>
public class PickyCustomer : Customer
{
    /// <summary>
    /// Tạo reviewer khó tính với kiên nhẫn tối đa 40 giây.
    /// </summary>
    public PickyCustomer(string name, Dish order) : base(name, order, 40) { }

    /// <summary>
    /// Tên loại khách.
    /// </summary>
    public override string TypeName => "Reviewer khó tính";

    /// <summary>
    /// Chờ quá 10 giây thì kiên nhẫn giảm gấp đôi, ngược lại giảm bình thường.
    /// </summary>
    protected override void ReducePatience(int seconds)
    {
        if (WaitedSeconds > 10)
        {
            SetPatience(Patience - seconds * 2);
        }
        else
        {
            SetPatience(Patience - seconds);
        }
    }

    /// <summary>
    /// Chờ không quá 10 giây thì tip 20% giá món, ngược lại không tip.
    /// </summary>
    public override int CalculateTip()
    {
        if (WaitedSeconds <= 10)
        {
            return Order.Price * 20 / 100;
        }
        return 0;
    }

    /// <summary>
    /// Câu nói tuỳ theo thời gian chờ: nhanh thì khen, chậm thì chê.
    /// </summary>
    public override string GetThankYouMessage()
    {
        if (WaitedSeconds <= 10)
        {
            return "Nhanh đấy, cho 5 sao!";
        }
        return "Chậm quá... thôi 3 sao.";
    }

    /// <summary>
    /// Câu nói khi reviewer bỏ về.
    /// </summary>
    public override string GetLeavingMessage()
    {
        return "Chờ lâu thế này, về viết review 1 sao!";
    }
}