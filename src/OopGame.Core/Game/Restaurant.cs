using OopGame.Core.Cooking;
using OopGame.Core.Customers;
using OopGame.Core.Menu;

namespace OopGame.Core.Game;

/// <summary>
/// Quán ăn: giữ tiền, uy tín, đồng hồ và điều phối thực đơn, khách, bếp, kho.
/// Giao diện chỉ gọi các phương thức public của lớp này, không tự tính tiền hay sửa kho.
/// </summary>
public class Restaurant
{
    /// <summary>Giờ mở cửa (08:00).</summary>
    public const int OpenHour = 8;

    /// <summary>Giờ đóng cửa (20:00).</summary>
    public const int CloseHour = 20;

    /// <summary>Mỗi lần Tick() là 10 phút trong game.</summary>
    public const int MinutesPerTick = 10;

    /// <summary>Tiền vốn lúc mở quán.</summary>
    public const int StartingMoney = 200000;

    /// <summary>Uy tín tối đa, cũng là uy tín lúc mở quán.</summary>
    public const int MaxReputation = 100;

    /// <summary>Số khách chờ tối đa cùng lúc.</summary>
    public const int MaxWaitingCustomers = 5;

    /// <summary>Số phần mỗi nguyên liệu có sẵn trong kho lúc mở quán.</summary>
    public const int StartingStockPerIngredient = 5;

    /// <summary>Uy tín bị trừ mỗi khi có khách bỏ về.</summary>
    public const int ReputationLostPerLeave = 10;

    /// <summary>Uy tín được cộng mỗi khi phục vụ xong một khách.</summary>
    public const int ReputationGainedPerServe = 2;

    /// <summary>Phần trăm khả năng có khách mới trong mỗi Tick().</summary>
    public const int CustomerChancePercent = 30;

    private readonly Random _random;
    private readonly CustomerFactory _customerFactory;
    private readonly List<Customer> _waitingCustomers = new List<Customer>();

    /// <summary>
    /// Mở quán với vốn ban đầu, kho có sẵn mỗi nguyên liệu 5 phần. Chưa mở cửa ngày nào.
    /// </summary>
    public Restaurant(Random random)
    {
        _random = random;
        _customerFactory = new CustomerFactory(random);
        Menu = new MenuBook();
        Inventory = new Inventory();
        Kitchen = new Kitchen(Inventory);
        Supplier = new Supplier();

        foreach (string ingredient in Ingredients.All)
        {
            Inventory.Add(ingredient, StartingStockPerIngredient);
        }

        Money = StartingMoney;
        Reputation = MaxReputation;
        Day = 0;
        MinutesOfDay = OpenHour * 60;
        IsOpen = false;
    }

    /// <summary>Thực đơn của quán.</summary>
    public MenuBook Menu { get; }

    /// <summary>Kho nguyên liệu.</summary>
    public Inventory Inventory { get; }

    /// <summary>Bếp, nấu tối đa 2 món cùng lúc.</summary>
    public Kitchen Kitchen { get; }

    /// <summary>Nhà cung cấp nguyên liệu.</summary>
    public Supplier Supplier { get; }

    /// <summary>Tiền hiện có.</summary>
    public int Money { get; private set; }

    /// <summary>Uy tín hiện tại, về 0 là thua.</summary>
    public int Reputation { get; private set; }

    /// <summary>Ngày thứ mấy, 0 khi chưa mở ngày nào.</summary>
    public int Day { get; private set; }

    /// <summary>Số phút tính từ 0 giờ, ví dụ 08:00 là 480.</summary>
    public int MinutesOfDay { get; private set; }

    /// <summary>Đồng hồ dạng "HH:mm".</summary>
    public string ClockText => (MinutesOfDay / 60).ToString("00") + ":" + (MinutesOfDay % 60).ToString("00");

    /// <summary>Quán có đang mở cửa không.</summary>
    public bool IsOpen { get; private set; }

    /// <summary>Uy tín về 0 là thua.</summary>
    public bool IsGameOver => Reputation <= 0;

    /// <summary>Khách đang chờ, chỉ đọc.</summary>
    public IReadOnlyList<Customer> WaitingCustomers => _waitingCustomers.AsReadOnly();

    /// <summary>Bảng tổng kết của ngày hiện tại, null khi chưa mở ngày nào.</summary>
    public DaySummary? CurrentSummary { get; private set; }

    /// <summary>
    /// Mở cửa một ngày mới. Trả về false nếu quán đang mở hoặc đã thua.
    /// </summary>
    public bool OpenDay()
    {
        if (IsOpen || IsGameOver)
        {
            return false;
        }

        Day++;
        MinutesOfDay = OpenHour * 60;
        IsOpen = true;
        CurrentSummary = new DaySummary(Day);
        return true;
    }

    /// <summary>
    /// Chạy một giây của game: đồng hồ chạy, bếp nấu, khách mất kiên nhẫn, có thể có khách mới.
    /// Trả về các dòng thông báo của giây này.
    /// </summary>
    public List<string> Tick()
    {
        List<string> logs = new List<string>();
        if (!IsOpen)
        {
            return logs;
        }

        MinutesOfDay += MinutesPerTick;

        int readyBefore = Kitchen.ReadyDishes.Count;
        Kitchen.Update(1);
        for (int i = readyBefore; i < Kitchen.ReadyDishes.Count; i++)
        {
            logs.Add("Ting! " + Kitchen.ReadyDishes[i].Name + " ra lò.");
        }

        UpdateWaitingCustomers(logs);
        TryAddNewCustomer(logs);

        if (IsGameOver)
        {
            IsOpen = false;
            _waitingCustomers.Clear();
            logs.Add("Uy tín về 0. Quán dính phốt, phải đóng cửa!");
        }
        else if (MinutesOfDay >= CloseHour * 60)
        {
            IsOpen = false;
            _waitingCustomers.Clear();
            logs.Add("20:00 rồi, đóng cửa đi ngủ thôi!");
        }

        return logs;
    }

    /// <summary>
    /// Bắt đầu nấu một món. Chỉ nấu được khi quán đang mở, bếp còn chỗ và đủ nguyên liệu.
    /// </summary>
    public bool StartCooking(Dish dish)
    {
        return IsOpen && Kitchen.StartCooking(dish);
    }

    /// <summary>
    /// Mang món đã nấu xong ra cho khách. Thành công thì nhận tiền món cộng tip và được cộng uy tín.
    /// </summary>
    public bool Serve(Customer customer)
    {
        if (!IsOpen || !_waitingCustomers.Contains(customer))
        {
            return false;
        }
        if (!Kitchen.TakeReadyDish(customer.Order))
        {
            return false;
        }

        int earned = customer.Order.Price + customer.CalculateTip();
        Money += earned;
        Reputation = Math.Min(MaxReputation, Reputation + ReputationGainedPerServe);
        if (CurrentSummary != null)
        {
            CurrentSummary.RecordServed(earned);
        }
        _waitingCustomers.Remove(customer);
        return true;
    }

    /// <summary>
    /// Mua nguyên liệu từ nhà cung cấp. Trả về false nếu số lượng sai, không có hàng hoặc không đủ tiền.
    /// Mua được cả lúc quán đóng cửa.
    /// </summary>
    public bool BuyIngredient(string ingredient, int amount)
    {
        if (amount <= 0)
        {
            return false;
        }

        bool isAvailable = false;
        foreach (string available in Supplier.GetAvailableIngredients())
        {
            if (available == ingredient)
            {
                isAvailable = true;
                break;
            }
        }
        if (!isAvailable)
        {
            return false;
        }

        int totalPrice = Supplier.GetTotalPrice(ingredient, amount);
        if (totalPrice > Money)
        {
            return false;
        }

        Money -= totalPrice;
        Inventory.Add(ingredient, amount);
        if (CurrentSummary != null)
        {
            CurrentSummary.RecordExpense(totalPrice);
        }
        return true;
    }

    // Duyệt bản sao vì có thể xoá khách khỏi danh sách gốc trong lúc duyệt.
    private void UpdateWaitingCustomers(List<string> logs)
    {
        List<Customer> snapshot = new List<Customer>(_waitingCustomers);
        foreach (Customer customer in snapshot)
        {
            customer.Update(1);
            if (customer.IsLeaving)
            {
                _waitingCustomers.Remove(customer);
                Reputation = Math.Max(0, Reputation - ReputationLostPerLeave);
                if (CurrentSummary != null)
                {
                    CurrentSummary.RecordLeft();
                }
                logs.Add(customer.Name + " bỏ về: \"" + customer.GetLeavingMessage() + "\" (-" + ReputationLostPerLeave + " uy tín)");
            }
        }
    }

    private void TryAddNewCustomer(List<string> logs)
    {
        if (_waitingCustomers.Count >= MaxWaitingCustomers)
        {
            return;
        }
        if (_random.Next(100) >= CustomerChancePercent)
        {
            return;
        }

        Customer customer = _customerFactory.CreateRandom(Menu.GetAll());
        _waitingCustomers.Add(customer);
        logs.Add(customer.Name + " (" + customer.TypeName + ") bước vào, gọi " + customer.Order.Name);
    }
}
