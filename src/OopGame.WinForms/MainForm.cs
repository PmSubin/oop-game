using System.Globalization;
using OopGame.Core.Cooking;
using OopGame.Core.Customers;
using OopGame.Core.Game;
using OopGame.Core.Menu;

namespace OopGame.WinForms;

// man hinh chinh cua quan
public partial class MainForm : Form
{
    private static readonly CultureInfo _vietnameseCulture = CultureInfo.GetCultureInfo("vi-VN");

    // mot nhip cua game (Restaurant.Tick) ung voi bao nhieu mili giay that
    // luat choi trong Core khong doi; chi dong ho ngoai giao dien chay cham lai
    private static readonly string[] _speedNames = { "Chậm (3 giây/nhịp)", "Vừa (2 giây/nhịp)", "Nhanh (1 giây/nhịp)" };
    private static readonly int[] _speedIntervalsMs = { 3000, 2000, 1000 };

    private const string EmptyCustomersText = "Chưa có khách nào. Nấu sẵn vài món để đón khách.";
    private const string EmptyKitchenText = "Bếp đang trống. Chọn món rồi bấm Nấu món.";

    private readonly Restaurant _restaurant = new Restaurant(new Random());
    private readonly System.Windows.Forms.Timer _timer = new System.Windows.Forms.Timer();

    // tao man hinh chinh, do thuc don va chuan bi dong ho
    public MainForm()
    {
        InitializeComponent();
        ApplyTheme();

        foreach (string speedName in _speedNames)
        {
            cboSpeed.Items.Add(speedName);
        }
        cboSpeed.SelectedIndex = 0;
        _timer.Tick += OnTimerTick;

        foreach (Dish dish in _restaurant.Menu.GetAll())
        {
            lstMenu.Items.Add(dish);
        }

        RefreshView();
        AddLog("Chào mừng đến Quán Ăn Bận Rộn! Bấm Mở cửa để bắt đầu.");
    }

    private void ApplyTheme()
    {
        BackColor = GameTheme.Background;
        ForeColor = GameTheme.Text;
        pnlHud.BackColor = GameTheme.Panel;

        lblDay.ForeColor = GameTheme.Text;
        lblClock.ForeColor = GameTheme.Orange;
        lblMoney.ForeColor = GameTheme.Yellow;
        lblReputation.ForeColor = GameTheme.Text;
        lblSpeed.ForeColor = GameTheme.TextMuted;
        cboSpeed.BackColor = GameTheme.PanelLight;
        cboSpeed.ForeColor = GameTheme.Text;

        lblCustomersTitle.ForeColor = GameTheme.Text;
        lblMenuTitle.ForeColor = GameTheme.Text;
        lblReadyTitle.ForeColor = GameTheme.Text;
        lblLogTitle.ForeColor = GameTheme.Text;
        lblSlogan.ForeColor = GameTheme.TextMuted;

        GameTheme.StyleList(lstCustomers, 80);
        GameTheme.StyleList(lstMenu, 42);
        GameTheme.StyleList(lstReady, 62);
        GameTheme.StyleList(lstLog, 25);

        GameTheme.StyleButton(btnOpen, GameTheme.Green);
        GameTheme.StyleButton(btnCook, GameTheme.Orange);
        GameTheme.StyleButton(btnServe, GameTheme.Blue);
        GameTheme.StyleButton(btnBuy, GameTheme.Yellow);
        GameTheme.StyleButton(btnHelp, GameTheme.PanelLight);
        btnHelp.ForeColor = GameTheme.Text;
    }

    private void AddLog(string message)
    {
        string prefix = _restaurant.IsOpen ? "[" + _restaurant.ClockText + "]  " : "";
        lstLog.Items.Add(prefix + message);
        lstLog.TopIndex = lstLog.Items.Count - 1;
    }

    private void RefreshView()
    {
        lblDay.Text = _restaurant.Day == 0 ? "Chưa mở" : "Ngày " + _restaurant.Day;
        lblClock.Text = _restaurant.ClockText;
        lblMoney.Text = "Tiền: " + FormatMoney(_restaurant.Money);
        lblReputation.Text = "Uy tín: " + _restaurant.Reputation + "/" + Restaurant.MaxReputation;
        pnlReputationBar.Invalidate();

        int waitingCount = _restaurant.WaitingCustomers.Count;
        lblCustomersTitle.Text = "Khách đang chờ  " + waitingCount + "/" + Restaurant.MaxWaitingCustomers;

        int cookingCount = _restaurant.Kitchen.CookingOrders.Count;
        int readyCount = _restaurant.Kitchen.ReadyDishes.Count;
        lblReadyTitle.Text = "Bếp  " + cookingCount + "/" + Kitchen.MaxSlots + " chảo, " + readyCount + " món xong";

        List<object> customerItems = new List<object>();
        foreach (Customer customer in _restaurant.WaitingCustomers)
        {
            customerItems.Add(customer);
        }
        if (customerItems.Count == 0)
        {
            customerItems.Add(EmptyCustomersText);
        }
        SyncItems(lstCustomers, customerItems);

        List<object> kitchenItems = new List<object>();
        foreach (CookingOrder order in _restaurant.Kitchen.CookingOrders)
        {
            kitchenItems.Add(order);
        }
        foreach (Dish dish in _restaurant.Kitchen.ReadyDishes)
        {
            kitchenItems.Add(dish);
        }
        if (kitchenItems.Count == 0)
        {
            kitchenItems.Add(EmptyKitchenText);
        }
        SyncItems(lstReady, kitchenItems);

        lstMenu.Invalidate();

        bool isOpen = _restaurant.IsOpen;
        GameTheme.SetButtonEnabled(btnOpen, !isOpen && !_restaurant.IsGameOver);
        GameTheme.SetButtonEnabled(btnCook, isOpen);
        GameTheme.SetButtonEnabled(btnServe, isOpen);
        GameTheme.SetButtonEnabled(btnBuy, true);
    }

    // chi dung lai danh sach khi noi dung doi; con lai chi ve lai, de khong nhay va khong mat dong dang chon
    private static void SyncItems(ListBox list, List<object> desired)
    {
        bool same = list.Items.Count == desired.Count;
        for (int i = 0; same && i < desired.Count; i++)
        {
            if (!ReferenceEquals(list.Items[i], desired[i]) && !Equals(list.Items[i], desired[i]))
            {
                same = false;
            }
        }
        if (same)
        {
            list.Invalidate();
            return;
        }

        object? selected = list.SelectedItem;
        list.BeginUpdate();
        list.Items.Clear();
        foreach (object item in desired)
        {
            list.Items.Add(item);
        }
        if (selected != null && list.Items.Contains(selected))
        {
            list.SelectedItem = selected;
        }
        list.EndUpdate();
    }

    private int ToRealSeconds(int gameSeconds)
    {
        return gameSeconds * _timer.Interval / 1000;
    }

    private bool HasReadyDish(Dish order)
    {
        foreach (Dish dish in _restaurant.Kitchen.ReadyDishes)
        {
            if (dish.Name == order.Name)
            {
                return true;
            }
        }
        return false;
    }

    private int WaitingCountFor(Dish dish)
    {
        int count = 0;
        foreach (Customer customer in _restaurant.WaitingCustomers)
        {
            if (customer.Order.Name == dish.Name)
            {
                count++;
            }
        }
        return count;
    }

    private List<string> MissingIngredients(Dish dish)
    {
        List<string> missing = new List<string>();
        foreach (KeyValuePair<string, int> item in dish.GetIngredients())
        {
            if (_restaurant.Inventory.GetAmount(item.Key) < item.Value)
            {
                missing.Add(item.Key);
            }
        }
        return missing;
    }

    // luat cua tung loai khach, noi bang giay that de khop voi thanh kien nhan
    // cac moc 20, 15, 10 nhip la cua Core (NormalCustomer, VipCustomer, PickyCustomer); doi o do thi sua o day
    private string TypeHint(Customer customer)
    {
        if (customer is VipCustomer)
        {
            return "Nóng tính · tip 30% trong " + ToRealSeconds(15) + "s, sau đó 10%";
        }
        if (customer is PickyCustomer)
        {
            return "Khó tính · tip 20% trong " + ToRealSeconds(10) + "s, rồi bực gấp đôi";
        }
        return "Dễ tính · tip 10% nếu phục vụ trong " + ToRealSeconds(20) + "s";
    }

    private static Color TypeColor(Customer customer)
    {
        if (customer is VipCustomer)
        {
            return GameTheme.Yellow;
        }
        if (customer is PickyCustomer)
        {
            return GameTheme.Purple;
        }
        return GameTheme.Blue;
    }

    private static void DrawPlaceholder(Graphics g, Rectangle b, string text)
    {
        Rectangle rect = new Rectangle(b.X + 14, b.Y, b.Width - 20, b.Height);
        GameTheme.DrawText(g, text, GameTheme.Body, rect, GameTheme.TextMuted);
    }

    private static void DrawStrip(Graphics g, Rectangle bounds, Color color)
    {
        using (SolidBrush brush = new SolidBrush(color))
        {
            g.FillRectangle(brush, new Rectangle(bounds.X, bounds.Y, 6, bounds.Height - 1));
        }
    }

    private void lstCustomers_DrawItem(object? sender, DrawItemEventArgs e)
    {
        if (e.Index < 0)
        {
            return;
        }
        GameTheme.DrawBuffered(e, lstCustomers.Items[e.Index], PaintCustomerItem);
    }

    private void PaintCustomerItem(Graphics g, Rectangle b, bool selected, object item)
    {
        GameTheme.DrawItemBackground(g, b, selected);

        Customer? customer = item as Customer;
        if (customer == null)
        {
            DrawPlaceholder(g, b, item.ToString() ?? "");
            return;
        }

        Color typeColor = TypeColor(customer);
        DrawStrip(g, b, typeColor);

        int x = b.X + 14;
        int right = b.Right - 12;

        // dong 1: ten khach va nhan loai khach
        Size nameSize = TextRenderer.MeasureText(g, customer.Name, GameTheme.BodyBold);
        int nameWidth = Math.Min(nameSize.Width, right - x - 140);
        GameTheme.DrawText(g, customer.Name, GameTheme.BodyBold, new Rectangle(x, b.Y + 6, nameWidth, 20), GameTheme.Text);
        GameTheme.DrawBadge(g, customer.TypeName, x + nameWidth + 8, b.Y + 8, typeColor, GameTheme.Background);

        // dong 2: mon khach goi; neu bep da co mon do thi bao ngay
        bool ready = HasReadyDish(customer.Order);
        int orderWidth = ready ? right - x - 120 : right - x;
        GameTheme.DrawText(g, "Gọi: " + customer.Order.Name, GameTheme.Body, new Rectangle(x, b.Y + 27, orderWidth, 18), GameTheme.TextMuted);
        if (ready)
        {
            GameTheme.DrawBadge(g, "MÓN ĐÃ XONG", right - 104, b.Y + 27, GameTheme.Green, GameTheme.Background);
        }

        // dong 3: luat cua loai khach nay, va tien tip neu phuc vu ngay luc nay (giam dan khi khach cho lau)
        GameTheme.DrawText(g, TypeHint(customer), GameTheme.Small, new Rectangle(x, b.Y + 46, right - x - 104, 16), typeColor);
        string tipText = "tip: " + FormatMoney(customer.CalculateTip());
        GameTheme.DrawText(g, tipText, GameTheme.SmallBold, new Rectangle(right - 100, b.Y + 46, 100, 16), GameTheme.Yellow, true);

        // dong 4: thanh kien nhan va so giay that con lai
        double ratio = (double)customer.Patience / customer.MaxPatience;
        Color barColor = GameTheme.BarColor(ratio);
        Rectangle bar = new Rectangle(x, b.Y + 67, right - x - 70, 7);
        GameTheme.DrawBar(g, bar, ratio, barColor);
        string timeText = "còn " + ToRealSeconds(customer.Patience) + "s";
        GameTheme.DrawText(g, timeText, GameTheme.SmallBold, new Rectangle(bar.Right + 6, b.Y + 62, 64, 16), barColor, true);
    }

    private void lstMenu_DrawItem(object? sender, DrawItemEventArgs e)
    {
        if (e.Index < 0)
        {
            return;
        }
        GameTheme.DrawBuffered(e, lstMenu.Items[e.Index], PaintMenuItem);
    }

    private void PaintMenuItem(Graphics g, Rectangle b, bool selected, object item)
    {
        GameTheme.DrawItemBackground(g, b, selected);

        Dish? dish = item as Dish;
        if (dish == null)
        {
            return;
        }

        int x = b.X + 10;
        int right = b.Right - 10;

        // dong 1: ten mon ben trai, gia ben phai
        GameTheme.DrawText(g, dish.Name, GameTheme.BodyBold, new Rectangle(x, b.Y + 4, right - x - 95, 18), GameTheme.Text);
        GameTheme.DrawText(g, FormatMoney(dish.Price), GameTheme.BodyBold, new Rectangle(right - 90, b.Y + 4, 90, 18), GameTheme.Yellow, true);

        // dong 2: thoi gian nau that va kho co du nguyen lieu khong
        List<string> missing = MissingIngredients(dish);
        string status;
        Color statusColor;
        if (missing.Count == 0)
        {
            status = "Nấu " + ToRealSeconds(dish.CookTimeSeconds) + "s  ·  đủ nguyên liệu";
            statusColor = GameTheme.Green;
        }
        else
        {
            status = "Nấu " + ToRealSeconds(dish.CookTimeSeconds) + "s  ·  thiếu: " + string.Join(", ", missing);
            statusColor = GameTheme.Red;
        }

        int waitingCount = WaitingCountFor(dish);
        int statusWidth = waitingCount > 0 ? right - x - 100 : right - x;
        GameTheme.DrawText(g, status, GameTheme.Small, new Rectangle(x, b.Y + 23, statusWidth, 15), statusColor);
        if (waitingCount > 0)
        {
            GameTheme.DrawBadge(g, waitingCount + " khách chờ", right - 88, b.Y + 21, GameTheme.Orange, GameTheme.Background);
        }
    }

    private void lstReady_DrawItem(object? sender, DrawItemEventArgs e)
    {
        if (e.Index < 0)
        {
            return;
        }
        GameTheme.DrawBuffered(e, lstReady.Items[e.Index], PaintKitchenItem);
    }

    private void PaintKitchenItem(Graphics g, Rectangle b, bool selected, object item)
    {
        GameTheme.DrawItemBackground(g, b, selected);

        int x = b.X + 14;
        int right = b.Right - 12;

        CookingOrder? order = item as CookingOrder;
        if (order != null)
        {
            DrawStrip(g, b, GameTheme.Orange);
            GameTheme.DrawText(g, "Đang nấu: " + order.Dish.Name, GameTheme.BodyBold, new Rectangle(x, b.Y + 8, right - x, 20), GameTheme.Text);

            double ratio = 1.0 - (double)order.RemainingSeconds / order.Dish.CookTimeSeconds;
            Rectangle bar = new Rectangle(x, b.Y + 38, right - x - 70, 9);
            GameTheme.DrawBar(g, bar, ratio, GameTheme.Orange);
            string timeText = "còn " + ToRealSeconds(order.RemainingSeconds) + "s";
            GameTheme.DrawText(g, timeText, GameTheme.SmallBold, new Rectangle(bar.Right + 6, b.Y + 34, 64, 16), GameTheme.Orange, true);
            return;
        }

        Dish? dish = item as Dish;
        if (dish != null)
        {
            DrawStrip(g, b, GameTheme.Green);
            GameTheme.DrawText(g, "XONG: " + dish.Name, GameTheme.BodyBold, new Rectangle(x, b.Y + 8, right - x, 20), GameTheme.Green);
            GameTheme.DrawText(g, "Chọn khách gọi món này rồi bấm Phục vụ", GameTheme.Small, new Rectangle(x, b.Y + 33, right - x, 16), GameTheme.TextMuted);
            return;
        }

        DrawPlaceholder(g, b, item.ToString() ?? "");
    }

    private void lstLog_DrawItem(object? sender, DrawItemEventArgs e)
    {
        if (e.Index < 0)
        {
            return;
        }
        GameTheme.DrawBuffered(e, lstLog.Items[e.Index], PaintLogItem);
    }

    private static void PaintLogItem(Graphics g, Rectangle b, bool selected, object item)
    {
        using (SolidBrush brush = new SolidBrush(GameTheme.Panel))
        {
            g.FillRectangle(brush, b);
        }
        string text = item.ToString() ?? "";
        Rectangle rect = new Rectangle(b.X + 10, b.Y, b.Width - 16, b.Height);
        GameTheme.DrawText(g, text, GameTheme.Body, rect, LogColor(text));
    }

    // to mau dong nhat ky theo noi dung de luc dong khach van liec ra tin xau (do) va tin tot (xanh)
    private static Color LogColor(string text)
    {
        if (text.Contains("bỏ về") || text.Contains("Uy tín về 0") || text.Contains("thua"))
        {
            return GameTheme.Red;
        }
        if (text.Contains("Ting!"))
        {
            return GameTheme.Orange;
        }
        if (text.Contains("(+"))
        {
            return GameTheme.Green;
        }
        if (text.Contains("bước vào"))
        {
            return GameTheme.Blue;
        }
        if (text.Contains("Không") || text.Contains("Chưa") || text.Contains("Hãy") || text.Contains("Thiếu"))
        {
            return GameTheme.Yellow;
        }
        return GameTheme.Text;
    }

    private void pnlReputationBar_Paint(object? sender, PaintEventArgs e)
    {
        double ratio = (double)_restaurant.Reputation / Restaurant.MaxReputation;
        GameTheme.DrawBar(e.Graphics, pnlReputationBar.ClientRectangle, ratio, GameTheme.BarColor(ratio));
    }

    private void cboSpeed_SelectedIndexChanged(object? sender, EventArgs e)
    {
        int index = cboSpeed.SelectedIndex;
        if (index < 0 || index >= _speedIntervalsMs.Length)
        {
            return;
        }
        _timer.Interval = _speedIntervalsMs[index];

        // so giay hien thi phu thuoc toc do, nen ve lai cac danh sach
        lstCustomers.Invalidate();
        lstMenu.Invalidate();
        lstReady.Invalidate();
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
            AddLog("Bắt đầu nấu " + dish.Name + ", xong sau " + ToRealSeconds(dish.CookTimeSeconds) + " giây.");
        }
        else if (!_restaurant.IsOpen)
        {
            AddLog("Không nấu được: quán chưa mở cửa.");
        }
        else if (_restaurant.Kitchen.IsFull)
        {
            AddLog("Không nấu được: bếp đang kín " + Kitchen.MaxSlots + " chảo, đợi món ra lò.");
        }
        else
        {
            AddLog("Thiếu nguyên liệu cho " + dish.Name + ": " + string.Join(", ", MissingIngredients(dish)) + ". Bấm Nhập hàng.");
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
        int moneyBefore = _restaurant.Money;
        using (BuyForm form = new BuyForm(_restaurant))
        {
            form.ShowDialog(this);
        }
        int spent = moneyBefore - _restaurant.Money;
        if (spent > 0)
        {
            AddLog("Đã nhập hàng hết " + FormatMoney(spent) + ".");
        }
        RefreshView();
    }

    private void btnHelp_Click(object? sender, EventArgs e)
    {
        using (HelpForm form = new HelpForm(_restaurant, _timer.Interval))
        {
            form.ShowDialog(this);
        }
    }

    private void lstMenu_SelectedIndexChanged(object? sender, EventArgs e)
    {
        Dish? dish = lstMenu.SelectedItem as Dish;
        lblSlogan.Text = dish != null ? dish.Slogan : "";
    }

    // chon khach thi tu chon luon mon khach goi, de bam nau mon la dung mon
    private void lstCustomers_SelectedIndexChanged(object? sender, EventArgs e)
    {
        Customer? customer = lstCustomers.SelectedItem as Customer;
        if (customer == null)
        {
            return;
        }
        foreach (object item in lstMenu.Items)
        {
            Dish? dish = item as Dish;
            if (dish != null && dish.Name == customer.Order.Name)
            {
                lstMenu.SelectedItem = dish;
                break;
            }
        }
    }

    // chon mot mon da xong trong bep thi tu chon khach dang cho mon do, de bam phuc vu la dung nguoi
    private void lstReady_SelectedIndexChanged(object? sender, EventArgs e)
    {
        Dish? dish = lstReady.SelectedItem as Dish;
        if (dish == null)
        {
            return;
        }
        Customer? current = lstCustomers.SelectedItem as Customer;
        if (current != null && current.Order.Name == dish.Name)
        {
            return;
        }
        foreach (object item in lstCustomers.Items)
        {
            Customer? customer = item as Customer;
            if (customer != null && customer.Order.Name == dish.Name)
            {
                lstCustomers.SelectedItem = customer;
                break;
            }
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
                MessageBox.Show(this, _restaurant.CurrentSummary.ToString(), "Tổng kết ngày " + _restaurant.Day);
            }
            if (_restaurant.IsGameOver)
            {
                MessageBox.Show(this, "Uy tín về 0. Bạn đã thua!", "Thua cuộc");
            }
            RefreshView();
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
