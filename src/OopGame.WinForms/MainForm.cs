using System.Globalization;
using OopGame.Core.Cooking;
using OopGame.Core.Customers;
using OopGame.Core.Game;
using OopGame.Core.Menu;

namespace OopGame.WinForms;

/// <summary>
/// Màn hình chính của quán. Chỉ hiển thị và gọi phương thức public của Restaurant;
/// toàn bộ luật chơi nằm trong OopGame.Core.
/// </summary>
public partial class MainForm : Form
{
    private static readonly CultureInfo _vietnameseCulture = CultureInfo.GetCultureInfo("vi-VN");

    private readonly Restaurant _restaurant = new Restaurant(new Random());
    private readonly System.Windows.Forms.Timer _timer = new System.Windows.Forms.Timer();

    /// <summary>
    /// Tạo màn hình chính, đổ thực đơn và chuẩn bị đồng hồ 1 giây.
    /// </summary>
    public MainForm()
    {
        InitializeComponent();
        _timer.Interval = 1000;
        _timer.Tick += OnTimerTick;

        foreach (Dish dish in _restaurant.Menu.GetAll())
        {
            lstMenu.Items.Add(dish);
        }

        RefreshView();
        AddLog("Chào mừng đến Quán Ăn Bận Rộn! Bấm Mở cửa để bắt đầu.");
    }

    private void AddLog(string message)
    {
        lstLog.Items.Add(message);
        lstLog.TopIndex = lstLog.Items.Count - 1;
    }

    private void RefreshView()
    {
        lblMoney.Text = "Tiền: " + FormatMoney(_restaurant.Money);
        lblReputation.Text = "Uy tín: " + _restaurant.Reputation;
        lblDay.Text = "Ngày " + _restaurant.Day;
        lblClock.Text = _restaurant.ClockText;

        // Giữ nguyên khách đang chọn nếu khách đó vẫn còn chờ.
        Customer? selectedCustomer = lstCustomers.SelectedItem as Customer;
        lstCustomers.BeginUpdate();
        lstCustomers.Items.Clear();
        foreach (Customer customer in _restaurant.WaitingCustomers)
        {
            lstCustomers.Items.Add(customer);
        }
        if (selectedCustomer != null && lstCustomers.Items.Contains(selectedCustomer))
        {
            lstCustomers.SelectedItem = selectedCustomer;
        }
        lstCustomers.EndUpdate();

        lstReady.BeginUpdate();
        lstReady.Items.Clear();
        foreach (CookingOrder order in _restaurant.Kitchen.CookingOrders)
        {
            lstReady.Items.Add("(đang nấu) " + order.ToString());
        }
        foreach (Dish dish in _restaurant.Kitchen.ReadyDishes)
        {
            lstReady.Items.Add("(xong) " + dish.Name);
        }
        lstReady.EndUpdate();
    }

    private void btnOpen_Click(object? sender, EventArgs e)
    {
        if (_restaurant.OpenDay())
        {
            _timer.Start();
            AddLog("Ngày " + _restaurant.Day + " bắt đầu, quán mở cửa!");
        }
        else if (_restaurant.IsGameOver)
        {
            AddLog("Bạn đã thua, không mở cửa được nữa.");
        }
        else
        {
            AddLog("Quán đang mở cửa rồi.");
        }
        RefreshView();
    }

    private void btnCook_Click(object? sender, EventArgs e)
    {
        Dish? dish = lstMenu.SelectedItem as Dish;
        if (dish == null)
        {
            AddLog("Hãy chọn một món trong thực đơn.");
        }
        else if (_restaurant.StartCooking(dish))
        {
            AddLog("Bắt đầu nấu " + dish.Name);
        }
        else
        {
            AddLog("Không nấu được: quán chưa mở, bếp đang đầy hoặc thiếu nguyên liệu.");
        }
        RefreshView();
    }

    private void btnServe_Click(object? sender, EventArgs e)
    {
        Customer? customer = lstCustomers.SelectedItem as Customer;
        if (customer == null)
        {
            AddLog("Hãy chọn một khách đang chờ.");
            RefreshView();
            return;
        }

        int moneyBefore = _restaurant.Money;
        if (_restaurant.Serve(customer))
        {
            int earned = _restaurant.Money - moneyBefore;
            AddLog(customer.Name + ": \"" + customer.GetThankYouMessage() + "\" (+" + FormatMoney(earned) + ")");
        }
        else
        {
            AddLog("Chưa phục vụ được " + customer.Name + ": món " + customer.Order.Name + " chưa nấu xong.");
        }
        RefreshView();
    }

    private void btnBuy_Click(object? sender, EventArgs e)
    {
        using (BuyForm form = new BuyForm(_restaurant))
        {
            form.ShowDialog(this);
        }
        RefreshView();
    }

    private void lstMenu_SelectedIndexChanged(object? sender, EventArgs e)
    {
        Dish? dish = lstMenu.SelectedItem as Dish;
        if (dish != null)
        {
            AddLog(dish.Name + ": " + dish.Slogan);
        }
    }

    private void OnTimerTick(object? sender, EventArgs e)
    {
        List<string> logs = _restaurant.Tick();
        foreach (string log in logs)
        {
            AddLog(log);
        }
        RefreshView();

        if (!_restaurant.IsOpen)
        {
            _timer.Stop();
            if (_restaurant.CurrentSummary != null)
            {
                MessageBox.Show(_restaurant.CurrentSummary.ToString(), "Tổng kết ngày");
            }
            if (_restaurant.IsGameOver)
            {
                MessageBox.Show("Uy tín về 0. Bạn đã thua!", "Thua cuộc");
                btnOpen.Enabled = false;
            }
        }
    }

    private void MainForm_FormClosed(object? sender, FormClosedEventArgs e)
    {
        _timer.Stop();
    }

    private static string FormatMoney(int amount)
    {
        return amount.ToString("N0", _vietnameseCulture) + "đ";
    }
}
