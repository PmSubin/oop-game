using System.Globalization;

namespace OopGame.Core.Game;

/// <summary>
/// Bảng tổng kết một ngày làm việc của quán: phục vụ bao nhiêu khách, bao nhiêu khách bỏ về, lời lỗ ra sao.
/// </summary>
public class DaySummary
{
    private static readonly CultureInfo _vietnameseCulture = CultureInfo.GetCultureInfo("vi-VN");

    /// <summary>
    /// Tạo bảng tổng kết trống cho ngày thứ <paramref name="day"/>.
    /// </summary>
    public DaySummary(int day)
    {
        Day = day;
    }

    /// <summary>
    /// Ngày thứ mấy.
    /// </summary>
    public int Day { get; }

    /// <summary>
    /// Số khách đã được phục vụ.
    /// </summary>
    public int CustomersServed { get; private set; }

    /// <summary>
    /// Số khách chờ lâu quá nên bỏ về.
    /// </summary>
    public int CustomersLeft { get; private set; }

    /// <summary>
    /// Tổng tiền thu được (giá món cộng tip).
    /// </summary>
    public int Revenue { get; private set; }

    /// <summary>
    /// Tổng tiền đã chi để nhập hàng.
    /// </summary>
    public int Expenses { get; private set; }

    /// <summary>
    /// Lợi nhuận trong ngày: doanh thu trừ chi phí.
    /// </summary>
    public int Profit => Revenue - Expenses;

    /// <summary>
    /// Ghi nhận một khách đã được phục vụ và số tiền thu từ khách đó.
    /// </summary>
    public void RecordServed(int amount)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Số tiền không được âm.");
        }

        CustomersServed++;
        Revenue += amount;
    }

    /// <summary>
    /// Ghi nhận một khách bỏ về.
    /// </summary>
    public void RecordLeft()
    {
        CustomersLeft++;
    }

    /// <summary>
    /// Ghi nhận một khoản chi nhập hàng.
    /// </summary>
    public void RecordExpense(int amount)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Số tiền không được âm.");
        }

        Expenses += amount;
    }

    /// <summary>
    /// Bảng tổng kết 6 dòng để hiện trong hộp thoại cuối ngày.
    /// </summary>
    public override string ToString()
    {
        return "Tổng kết ngày " + Day + "\n"
            + "Đã phục vụ: " + CustomersServed + " khách\n"
            + "Khách bỏ về: " + CustomersLeft + " khách\n"
            + "Doanh thu: " + FormatMoney(Revenue) + "\n"
            + "Chi phí: " + FormatMoney(Expenses) + "\n"
            + "Lợi nhuận: " + FormatMoney(Profit);
    }

    private static string FormatMoney(int amount)
    {
        return amount.ToString("N0", _vietnameseCulture) + "đ";
    }
}
