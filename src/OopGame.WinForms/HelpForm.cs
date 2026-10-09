using System.Globalization;
using OopGame.Core.Cooking;
using OopGame.Core.Customers;
using OopGame.Core.Game;
using OopGame.Core.Menu;

namespace OopGame.WinForms;

// bang huong dan: cach choi, luat quan, ba loai khach, thuc don va gia nguyen lieu
public partial class HelpForm : Form
{
    private static readonly CultureInfo _vietnameseCulture = CultureInfo.GetCultureInfo("vi-VN");

    private readonly Restaurant _restaurant;
    private readonly int _tickIntervalMs;

    // tao bang huong dan cho quan dang choi, voi toc do hien tai de quy ra giay that
    public HelpForm(Restaurant restaurant, int tickIntervalMs)
    {
        InitializeComponent();
        _restaurant = restaurant;
        _tickIntervalMs = tickIntervalMs;
        ApplyTheme();
        BuildContent();
        rtbHelp.SelectionStart = 0;
        rtbHelp.ScrollToCaret();
    }

    private void ApplyTheme()
    {
        BackColor = GameTheme.Background;
        rtbHelp.BackColor = GameTheme.Background;
        rtbHelp.ForeColor = GameTheme.Text;
        GameTheme.StyleButton(btnClose, GameTheme.Orange);
    }

    private int Seconds(int ticks)
    {
        return ticks * _tickIntervalMs / 1000;
    }

    private string Ticks(int ticks)
    {
        return ticks + " nhịp (" + Seconds(ticks) + " giây ở tốc độ hiện tại)";
    }

    private void Heading(string text)
    {
        rtbHelp.SelectionFont = GameTheme.Title;
        rtbHelp.SelectionColor = GameTheme.Orange;
        rtbHelp.AppendText(text + Environment.NewLine);
    }

    private void Line(string text)
    {
        Line(text, GameTheme.Text);
    }

    private void Line(string text, Color color)
    {
        rtbHelp.SelectionFont = GameTheme.Body;
        rtbHelp.SelectionColor = color;
        rtbHelp.AppendText(text + Environment.NewLine);
    }

    private void Blank()
    {
        rtbHelp.AppendText(Environment.NewLine);
    }

    private void BuildContent()
    {
        Heading("Cách chơi");
        Line("1. Bấm Mở cửa. Đồng hồ chạy từ " + Restaurant.OpenHour + ":00 đến " + Restaurant.CloseHour + ":00, mỗi nhịp là " + Restaurant.MinutesPerTick + " phút trong game.");
        Line("2. Khách lần lượt vào, mỗi khách gọi một món và có thanh kiên nhẫn giảm dần.");
        Line("3. Bấm vào khách (món khách gọi tự được chọn), rồi bấm Nấu món. Nấu tốn nguyên liệu.");
        Line("4. Món xong thì khách chờ món đó hiện nhãn MÓN ĐÃ XONG. Chọn khách, bấm Phục vụ để nhận tiền và tip.");
        Line("5. Khách chờ lâu quá sẽ bỏ về và quán mất uy tín. Uy tín về 0 là thua.");
        Line("6. Hết nguyên liệu thì bấm Nhập hàng. Mua được cả lúc quán đóng cửa.");
        Line("7. Cuối ngày có bảng tổng kết. Bấm Mở cửa để sang ngày mới.");
        Blank();

        Heading("Luật quán");
        Line("Vốn ban đầu: " + FormatMoney(Restaurant.StartingMoney) + ". Uy tín ban đầu và tối đa: " + Restaurant.MaxReputation + ".");
        Line("Phục vụ xong một khách: +" + Restaurant.ReputationGainedPerServe + " uy tín. Khách bỏ về: -" + Restaurant.ReputationLostPerLeave + " uy tín.");
        Line("Tối đa " + Restaurant.MaxWaitingCustomers + " khách chờ cùng lúc. Mỗi nhịp có " + Restaurant.CustomerChancePercent + "% khả năng có khách mới.");
        Line("Bếp nấu tối đa " + Kitchen.MaxSlots + " món cùng lúc. Kho ban đầu có " + Restaurant.StartingStockPerIngredient + " phần mỗi nguyên liệu.");
        Line("Tốc độ: một nhịp đang bằng " + (_tickIntervalMs / 1000.0).ToString("0.#", _vietnameseCulture) + " giây thật. Đổi ở ô Tốc độ góc trên phải; luật chơi không đổi, chỉ nhanh chậm.");
        Blank();

        Heading("Ba loại khách (khác nhau ở kiên nhẫn và tiền tip)");
        Dish sampleDish = _restaurant.Menu.GetAll()[0];
        DescribeCustomer(new NormalCustomer("Khách mẫu", sampleDish), GameTheme.Blue,
            "Tip 10% giá món nếu được phục vụ trong " + Ticks(20) + ", chậm hơn thì không tip.",
            "Kiên nhẫn giảm đều.");
        DescribeCustomer(new VipCustomer("Khách mẫu", sampleDish), GameTheme.Yellow,
            "Tip 30% giá món nếu được phục vụ trong " + Ticks(15) + ", chậm hơn thì còn 10%.",
            "Chờ ngắn nhất trong ba loại: ưu tiên phục vụ trước.");
        DescribeCustomer(new PickyCustomer("Khách mẫu", sampleDish), GameTheme.Purple,
            "Tip 20% giá món nếu được phục vụ trong " + Ticks(10) + ", chậm hơn thì không tip.",
            "Chờ quá " + Ticks(10) + " thì kiên nhẫn giảm gấp đôi mỗi nhịp.");
        Line("Tỉ lệ xuất hiện: 60% vãng lai, 20% đại gia, 20% reviewer. Trên thẻ khách, dòng \"tip ngay\" là số tiền tip nếu phục vụ đúng lúc này.", GameTheme.TextMuted);
        Blank();

        Heading("Thực đơn");
        foreach (Dish dish in _restaurant.Menu.GetAll())
        {
            List<string> parts = new List<string>();
            foreach (KeyValuePair<string, int> item in dish.GetIngredients())
            {
                parts.Add(item.Key + (item.Value > 1 ? " x" + item.Value : ""));
            }
            Line(dish.Name + ": " + FormatMoney(dish.Price) + ", nấu " + Ticks(dish.CookTimeSeconds) + ".");
            Line("    Nguyên liệu: " + string.Join(", ", parts) + ". \"" + dish.Slogan + "\"", GameTheme.TextMuted);
        }
        Blank();

        Heading("Giá nguyên liệu (nhà cung cấp)");
        foreach (string ingredient in _restaurant.Supplier.GetAvailableIngredients())
        {
            Line(ingredient + ": " + FormatMoney(_restaurant.Supplier.GetPrice(ingredient)) + " một phần. Đang có trong kho: " + _restaurant.Inventory.GetAmount(ingredient) + ".");
        }
    }

    private void DescribeCustomer(Customer sample, Color color, string tipRule, string patienceRule)
    {
        Line(sample.TypeName + ": chờ tối đa " + Ticks(sample.MaxPatience) + ".", color);
        Line("    " + tipRule);
        Line("    " + patienceRule);
        Line("    Khi hài lòng nói: \"" + sample.GetThankYouMessage() + "\". Khi bỏ về nói: \"" + sample.GetLeavingMessage() + "\".", GameTheme.TextMuted);
    }

    private void btnClose_Click(object? sender, EventArgs e)
    {
        Close();
    }

    private static string FormatMoney(int amount)
    {
        return amount.ToString("N0", _vietnameseCulture) + "đ";
    }
}
