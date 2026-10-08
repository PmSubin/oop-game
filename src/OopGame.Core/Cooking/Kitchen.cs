using OopGame.Core.Menu;

namespace OopGame.Core.Cooking;

/// <summary>
/// Cái bếp của quán: nấu tối đa 2 món cùng lúc, dùng nguyên liệu trong kho.
/// Mỗi giây quán gọi Update(1) để đếm ngược thời gian nấu.
/// </summary>
public class Kitchen : IUpdatable
{
    /// <summary>
    /// Số món tối đa nấu cùng lúc.
    /// </summary>
    public const int MaxSlots = 2;                     // nấu tối đa 2 món cùng lúc

    private readonly Inventory _inventory;
    private readonly List<CookingOrder> _cookingOrders = new List<CookingOrder>();
    private readonly List<Dish> _readyDishes = new List<Dish>();

    /// <summary>
    /// Tạo bếp, dùng chung kho nguyên liệu được đưa vào.
    /// </summary>
    public Kitchen(Inventory inventory)
    {
        _inventory = inventory;
    }

    /// <summary>
    /// Các món đang nấu (chỉ đọc).
    /// </summary>
    public IReadOnlyList<CookingOrder> CookingOrders => _cookingOrders.AsReadOnly();

    /// <summary>
    /// Các món đã nấu xong, đang chờ phục vụ (chỉ đọc).
    /// </summary>
    public IReadOnlyList<Dish> ReadyDishes => _readyDishes.AsReadOnly();

    /// <summary>
    /// Bếp đã kín chảo chưa.
    /// </summary>
    public bool IsFull => _cookingOrders.Count >= MaxSlots;

    /// <summary>
    /// Bắt đầu nấu một món. Bếp đầy hoặc thiếu nguyên liệu thì trả về false.
    /// </summary>
    public bool StartCooking(Dish dish)
    {
        if (IsFull)
        {
            return false;
        }

        if (!_inventory.TryConsume(dish))
        {
            return false;
        }

        _cookingOrders.Add(new CookingOrder(dish));
        return true;
    }

    /// <summary>
    /// Trôi qua một số giây: đếm ngược mọi món đang nấu, món nào xong thì chuyển sang danh sách đã nấu xong.
    /// </summary>
    public void Update(int elapsedSeconds)
    {
        // Gom các món đã xong vào danh sách tạm, vì không được xoá khỏi List khi đang foreach chính nó.
        List<CookingOrder> finishedOrders = new List<CookingOrder>();

        foreach (CookingOrder order in _cookingOrders)
        {
            order.Advance(elapsedSeconds);
            if (order.IsDone)
            {
                finishedOrders.Add(order);
            }
        }

        foreach (CookingOrder finished in finishedOrders)
        {
            _cookingOrders.Remove(finished);
            _readyDishes.Add(finished.Dish);
        }
    }

    /// <summary>
    /// Lấy một món đã nấu xong ra phục vụ (so sánh theo tên món). Có thì trả về true, không có thì false.
    /// </summary>
    public bool TakeReadyDish(Dish dish)
    {
        for (int i = 0; i < _readyDishes.Count; i++)
        {
            if (_readyDishes[i].Name == dish.Name)
            {
                _readyDishes.RemoveAt(i);
                return true;
            }
        }
        return false;
    }
}