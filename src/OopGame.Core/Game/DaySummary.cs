using System.Globalization;

namespace OopGame.Core.Game;

// bang tong ket mot ngay lam viec cua quan: phuc vu bao nhieu khach, bao nhieu khach bo ve, loi lo ra sao
public class DaySummary
{
    private static readonly CultureInfo _vietnameseCulture = CultureInfo.GetCultureInfo("vi-VN");

    // tao bang tong ket trong cho ngay thu day
    public DaySummary(int day)
    {
        Day = day;
    }

    // ngay thu may
    public int Day { get; }

    // so khach da duoc phuc vu
    public int CustomersServed { get; private set; }

    // so khach cho lau qua nen bo ve
    public int CustomersLeft { get; private set; }

    // tong tien thu duoc (gia mon cong tip)
    public int Revenue { get; private set; }

    // tong tien da chi de nhap hang
    public int Expenses { get; private set; }

    // loi nhuan trong ngay: doanh thu tru chi phi
    public int Profit => Revenue - Expenses;

    // ghi nhan mot khach da duoc phuc vu va so tien thu tu khach do
    public void RecordServed(int amount)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Số tiền không được âm.");
        }

        CustomersServed++;
        Revenue += amount;
    }

    // ghi nhan mot khach bo ve
    public void RecordLeft()
    {
        CustomersLeft++;
    }

    // ghi nhan mot khoan chi nhap hang
    public void RecordExpense(int amount)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Số tiền không được âm.");
        }

        Expenses += amount;
    }

    // bang tong ket 6 dong de hien trong hop thoai cuoi ngay
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
