using OopGame.Core.Menu;

namespace OopGame.Core.Cooking;

// cai bep cua quan: nau toi da 2 mon cung luc, dung nguyen lieu trong kho
public class Kitchen : IUpdatable
{
    // so mon toi da nau cung luc
    public const int MaxSlots = 2; // nau toi da 2 mon cung luc

    private readonly Inventory _inventory;
    private readonly List<CookingOrder> _cookingOrders = new List<CookingOrder>();
    private readonly List<Dish> _readyDishes = new List<Dish>();

    // tao bep, dung chung kho nguyen lieu duoc dua vao
    public Kitchen(Inventory inventory)
    {
        _inventory = inventory;
    }

    // cac mon dang nau (chi doc)
    public IReadOnlyList<CookingOrder> CookingOrders => _cookingOrders.AsReadOnly();

    // cac mon da nau xong, dang cho phuc vu (chi doc)
    public IReadOnlyList<Dish> ReadyDishes => _readyDishes.AsReadOnly();

    // bep da kin chao chua
    public bool IsFull => _cookingOrders.Count >= MaxSlots;

    // bat dau nau mot mon
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

    // troi qua mot so giay: dem nguoc moi mon dang nau, mon nao xong thi chuyen sang danh sach da nau xong
    public void Update(int elapsedSeconds)
    {
        // gom cac mon da xong vao danh sach tam, vi khong duoc xoa khoi List khi dang foreach chinh no
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

    // lay mot mon da nau xong ra phuc vu (so sanh theo ten mon)
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