using OopGame.Core.Menu;

namespace OopGame.Core.Cooking;

/// <summary>
/// Một món đang nấu trong bếp: nhớ món nào và còn bao nhiêu giây nữa thì xong.
/// </summary>
public class CookingOrder
{
    /// <summary>
    /// Bắt đầu nấu một món. Thời gian còn lại ban đầu bằng thời gian nấu của món.
    /// </summary>
    public CookingOrder(Dish dish)
    {
        Dish = dish;
        RemainingSeconds = dish.CookTimeSeconds;
    }

    /// <summary>
    /// Món đang được nấu.
    /// </summary>
    public Dish Dish { get; }

    /// <summary>
    /// Số giây còn lại để nấu xong.
    /// </summary>
    public int RemainingSeconds { get; private set; }

    /// <summary>
    /// Món đã nấu xong chưa (hết thời gian chờ).
    /// </summary>
    public bool IsDone => RemainingSeconds == 0;

    /// <summary>
    /// Trôi qua một số giây. Thời gian còn lại không bao giờ xuống dưới 0.
    /// </summary>
    public void Advance(int seconds)
    {
        RemainingSeconds = Math.Max(0, RemainingSeconds - seconds);
    }

    /// <summary>
    /// Mô tả món đang nấu, ví dụ: "Phở Gõ Deadline - còn 3 giây".
    /// </summary>
    public override string ToString()
    {
        return $"{Dish.Name} - còn {RemainingSeconds} giây";
    }
}