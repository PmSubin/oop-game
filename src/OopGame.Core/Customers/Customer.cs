using OopGame.Core.Menu;

namespace OopGame.Core.Customers;

// lop cha truu tuong cho moi khach trong quan
public abstract class Customer : IUpdatable
{
    // ten khach
    public string Name { get; }

    // mon khach da goi
    public Dish Order { get; }

    // so giay kien nhan toi da
    public int MaxPatience { get; }

    // kien nhan con lai, luon nam trong khoang 0 den MaxPatience
    public int Patience { get; private set; }

    // so giay khach da cho
    public int WaitedSeconds { get; private set; }

    // het kien nhan thi khach bo ve
    public bool IsLeaving => Patience == 0;

    // ten loai khach de hien len man hinh
    public abstract string TypeName { get; }

    // tao mot khach moi: kien nhan day, chua cho giay nao
    protected Customer(string name, Dish order, int maxPatience)
    {
        Name = name;
        Order = order;
        MaxPatience = maxPatience;
        Patience = maxPatience;
        WaitedSeconds = 0;
    }

    // quan goi moi giay: khach cho them va kien nhan giam di
    public void Update(int elapsedSeconds)
    {
        if (elapsedSeconds <= 0)
        {
            return;
        }

        // thu tu quan trong: tang thoi gian cho truoc, giam kien nhan sau
        WaitedSeconds += elapsedSeconds;
        ReducePatience(elapsedSeconds);
    }

    // giam kien nhan
    protected virtual void ReducePatience(int seconds)
    {
        SetPatience(Patience - seconds);
    }

    // dat kien nhan, tu giu trong khoang 0 den MaxPatience
    protected void SetPatience(int value)
    {
        Patience = Math.Clamp(value, 0, MaxPatience);
    }

    // tien tip khi duoc phuc vu, moi loai khach tu tinh
    public abstract int CalculateTip();

    // cau khach noi khi duoc phuc vu
    public abstract string GetThankYouMessage();

    // cau khach noi khi het kien nhan bo ve
    public abstract string GetLeavingMessage();

    // mo ta khach, vi du: "anh shipper (dai gia) goi pho go deadline - kien nhan 20/25"
    public override string ToString()
    {
        return $"{Name} ({TypeName}) gọi {Order.Name} - kiên nhẫn {Patience}/{MaxPatience}";
    }
}