using OopGame.Core.Cooking;
using OopGame.Core.Customers;
using OopGame.Core.Menu;

namespace OopGame.Core.Game;

// quan an: giu tien, uy tin, dong ho va dieu phoi thuc don, khach, bep, kho
public class Restaurant
{
    // gio mo cua (08:00)
    public const int OpenHour = 8;

    // gio dong cua (20:00)
    public const int CloseHour = 20;

    // moi lan Tick() la 10 phut trong game
    public const int MinutesPerTick = 10;

    // tien von luc mo quan
    public const int StartingMoney = 200000;

    // uy tin toi da, cung la uy tin luc mo quan
    public const int MaxReputation = 100;

    // so khach cho toi da cung luc
    public const int MaxWaitingCustomers = 5;

    // so phan moi nguyen lieu co san trong kho luc mo quan
    public const int StartingStockPerIngredient = 5;

    // uy tin bi tru moi khi co khach bo ve
    public const int ReputationLostPerLeave = 10;

    // uy tin duoc cong moi khi phuc vu xong mot khach
    public const int ReputationGainedPerServe = 2;

    // phan tram kha nang co khach moi trong moi Tick()
    public const int CustomerChancePercent = 30;

    private readonly Random _random;
    private readonly CustomerFactory _customerFactory;
    private readonly List<Customer> _waitingCustomers = new List<Customer>();

    // mo quan voi von ban dau, kho co san moi nguyen lieu 5 phan
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

    // thuc don cua quan
    public MenuBook Menu { get; }

    // kho nguyen lieu
    public Inventory Inventory { get; }

    // bep, nau toi da 2 mon cung luc
    public Kitchen Kitchen { get; }

    // nha cung cap nguyen lieu
    public Supplier Supplier { get; }

    // tien hien co
    public int Money { get; private set; }

    // uy tin hien tai, ve 0 la thua
    public int Reputation { get; private set; }

    // ngay thu may, 0 khi chua mo ngay nao
    public int Day { get; private set; }

    // so phut tinh tu 0 gio, vi du 08:00 la 480
    public int MinutesOfDay { get; private set; }

    // dong ho dang "hh:mm"
    public string ClockText => (MinutesOfDay / 60).ToString("00") + ":" + (MinutesOfDay % 60).ToString("00");

    // quan co dang mo cua khong
    public bool IsOpen { get; private set; }

    // uy tin ve 0 la thua
    public bool IsGameOver => Reputation <= 0;

    // khach dang cho, chi doc
    public IReadOnlyList<Customer> WaitingCustomers => _waitingCustomers.AsReadOnly();

    // bang tong ket cua ngay hien tai, null khi chua mo ngay nao
    public DaySummary? CurrentSummary { get; private set; }

    // mo cua mot ngay moi
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

    // chay mot giay cua game: dong ho chay, bep nau, khach mat kien nhan, co the co khach moi
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

    // bat dau nau mot mon
    public bool StartCooking(Dish dish)
    {
        return IsOpen && Kitchen.StartCooking(dish);
    }

    // mang mon da nau xong ra cho khach
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

    // mua nguyen lieu tu nha cung cap
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

    // duyet ban sao vi co the xoa khach khoi danh sach goc trong luc duyet
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
