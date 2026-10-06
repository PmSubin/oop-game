using OopGame.Core.Menu;

namespace OopGame.Core.Customers;

/// <summary>
/// Lớp cha trừu tượng cho mọi khách trong quán.
/// Không tạo trực tiếp được, phải tạo qua lớp con (NormalCustomer, VipCustomer, PickyCustomer).
/// </summary>
public abstract class Customer : IUpdatable
{
    /// <summary>
    /// Tên khách.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Món khách đã gọi.
    /// </summary>
    public Dish Order { get; }

    /// <summary>
    /// Số giây kiên nhẫn tối đa.
    /// </summary>
    public int MaxPatience { get; }

    /// <summary>
    /// Kiên nhẫn còn lại, luôn nằm trong khoảng 0 đến MaxPatience. Bên ngoài chỉ được đọc.
    /// </summary>
    public int Patience { get; private set; }

    /// <summary>
    /// Số giây khách đã chờ.
    /// </summary>
    public int WaitedSeconds { get; private set; }

    /// <summary>
    /// Hết kiên nhẫn thì khách bỏ về.
    /// </summary>
    public bool IsLeaving => Patience == 0;

    /// <summary>
    /// Tên loại khách để hiện lên màn hình.
    /// </summary>
    public abstract string TypeName { get; }

    /// <summary>
    /// Tạo một khách mới: kiên nhẫn đầy, chưa chờ giây nào.
    /// </summary>
    protected Customer(string name, Dish order, int maxPatience)
    {
        Name = name;
        Order = order;
        MaxPatience = maxPatience;
        Patience = maxPatience;
        WaitedSeconds = 0;
    }

    /// <summary>
    /// Quán gọi mỗi giây: khách chờ thêm và kiên nhẫn giảm đi.
    /// </summary>
    public void Update(int elapsedSeconds)
    {
        if (elapsedSeconds <= 0)
        {
            return;
        }

        // Thứ tự quan trọng: tăng thời gian chờ trước, giảm kiên nhẫn sau.
        WaitedSeconds += elapsedSeconds;
        ReducePatience(elapsedSeconds);
    }

    /// <summary>
    /// Giảm kiên nhẫn. Mặc định mỗi giây giảm 1; lớp con có thể viết lại cách giảm.
    /// </summary>
    protected virtual void ReducePatience(int seconds)
    {
        SetPatience(Patience - seconds);
    }

    /// <summary>
    /// Đặt kiên nhẫn, tự giữ trong khoảng 0 đến MaxPatience.
    /// Lớp con muốn đổi Patience thì phải gọi hàm này.
    /// </summary>
    protected void SetPatience(int value)
    {
        Patience = Math.Clamp(value, 0, MaxPatience);
    }

    /// <summary>
    /// Tiền tip khi được phục vụ, mỗi loại khách tự tính.
    /// </summary>
    public abstract int CalculateTip();

    /// <summary>
    /// Câu khách nói khi được phục vụ.
    /// </summary>
    public abstract string GetThankYouMessage();

    /// <summary>
    /// Câu khách nói khi hết kiên nhẫn bỏ về.
    /// </summary>
    public abstract string GetLeavingMessage();

    /// <summary>
    /// Mô tả khách, ví dụ: "Anh Shipper (Đại gia) gọi Phở Gõ Deadline - kiên nhẫn 20/25".
    /// </summary>
    public override string ToString()
    {
        return $"{Name} ({TypeName}) gọi {Order.Name} - kiên nhẫn {Patience}/{MaxPatience}";
    }
}