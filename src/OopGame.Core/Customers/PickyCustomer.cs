using OopGame.Core.Menu;

namespace OopGame.Core.Customers;

// reviewer kho tinh: cho qua 10 giay la buc gap doi
public class PickyCustomer : Customer
{
    // tao reviewer kho tinh voi kien nhan toi da 40 giay
    public PickyCustomer(string name, Dish order) : base(name, order, 40) { }

    // ten loai khach
    public override string TypeName => "Reviewer khó tính";

    // cho qua 10 giay thi kien nhan giam gap doi, nguoc lai giam binh thuong
    protected override void ReducePatience(int seconds)
    {
        if (WaitedSeconds > 10)
        {
            SetPatience(Patience - seconds * 2);
        }
        else
        {
            SetPatience(Patience - seconds);
        }
    }

    // cho khong qua 10 giay thi tip 20% gia mon, nguoc lai khong tip
    public override int CalculateTip()
    {
        if (WaitedSeconds <= 10)
        {
            return Order.Price * 20 / 100;
        }
        return 0;
    }

    // cau noi tuy theo thoi gian cho: nhanh thi khen, cham thi che
    public override string GetThankYouMessage()
    {
        if (WaitedSeconds <= 10)
        {
            return "Nhanh đấy, cho 5 sao!";
        }
        return "Chậm quá... thôi 3 sao.";
    }

    // cau noi khi reviewer bo ve
    public override string GetLeavingMessage()
    {
        return "Chờ lâu thế này, về viết review 1 sao!";
    }
}