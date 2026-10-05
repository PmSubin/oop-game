using OopGame.Core;
using OopGame.Core.Customers;
using OopGame.Core.Menu;

namespace OopGame.Tests;

// Bài kiểm tra cho Phần 2: Khách hàng. Không sửa file này.
public class Part2CustomerTests
{
    // Món giả giá 100.000đ để dễ tính tip. Không phụ thuộc Phần 1.
    private sealed class FakeDish : Dish
    {
        public FakeDish() : base("Món thử", 100000, 3) { }

        public override IReadOnlyDictionary<string, int> GetIngredients()
        {
            return new Dictionary<string, int> { [Ingredients.Tea] = 1 };
        }
    }

    private static void Wait(Customer customer, int seconds)
    {
        for (int i = 0; i < seconds; i++)
        {
            customer.Update(1);
        }
    }

    [Fact]
    public void Customer_IsAbstract_AndImplementsIUpdatable()
    {
        Assert.True(typeof(Customer).IsAbstract);
        Assert.True(typeof(IUpdatable).IsAssignableFrom(typeof(Customer)));
    }

    [Fact]
    public void NewCustomer_StartsWithFullPatience()
    {
        Customer customer = new NormalCustomer("Anh Minh", new FakeDish());
        Assert.Equal("Anh Minh", customer.Name);
        Assert.Equal("Món thử", customer.Order.Name);
        Assert.Equal(40, customer.MaxPatience);
        Assert.Equal(40, customer.Patience);
        Assert.Equal(0, customer.WaitedSeconds);
        Assert.False(customer.IsLeaving);
    }

    [Fact]
    public void TypeNames_AreCorrect()
    {
        Assert.Equal("Khách thường", new NormalCustomer("A", new FakeDish()).TypeName);
        Assert.Equal("Khách VIP", new VipCustomer("A", new FakeDish()).TypeName);
        Assert.Equal("Khách khó tính", new PickyCustomer("A", new FakeDish()).TypeName);
    }

    [Fact]
    public void Update_WithZeroOrNegative_DoesNothing()
    {
        Customer customer = new NormalCustomer("A", new FakeDish());
        customer.Update(0);
        customer.Update(-5);
        Assert.Equal(40, customer.Patience);
        Assert.Equal(0, customer.WaitedSeconds);
    }

    [Fact]
    public void NormalCustomer_LosesOnePatiencePerSecond()
    {
        Customer customer = new NormalCustomer("A", new FakeDish());
        Wait(customer, 15);
        Assert.Equal(25, customer.Patience);
        Assert.Equal(15, customer.WaitedSeconds);
    }

    [Fact]
    public void VipCustomer_LeavesAfter25Seconds()
    {
        Customer customer = new VipCustomer("A", new FakeDish());
        Wait(customer, 24);
        Assert.False(customer.IsLeaving);
        customer.Update(1);
        Assert.True(customer.IsLeaving);
    }

    [Fact]
    public void PickyCustomer_LosesDoubleAfterTenSeconds()
    {
        Customer customer = new PickyCustomer("A", new FakeDish());
        Wait(customer, 10);
        Assert.Equal(30, customer.Patience);
        Wait(customer, 5);
        Assert.Equal(20, customer.Patience);
    }

    [Fact]
    public void Patience_NeverGoesBelowZero()
    {
        Customer customer = new PickyCustomer("A", new FakeDish());
        Wait(customer, 100);
        Assert.Equal(0, customer.Patience);
        Assert.True(customer.IsLeaving);
    }

    [Fact]
    public void Tips_FollowTheTable()
    {
        Customer normal = new NormalCustomer("A", new FakeDish());
        Wait(normal, 20);
        Assert.Equal(10000, normal.CalculateTip());
        normal.Update(1);
        Assert.Equal(0, normal.CalculateTip());

        Customer vip = new VipCustomer("A", new FakeDish());
        Wait(vip, 15);
        Assert.Equal(30000, vip.CalculateTip());
        vip.Update(1);
        Assert.Equal(10000, vip.CalculateTip());

        Customer picky = new PickyCustomer("A", new FakeDish());
        Wait(picky, 10);
        Assert.Equal(20000, picky.CalculateTip());
        picky.Update(1);
        Assert.Equal(0, picky.CalculateTip());
    }

    [Fact]
    public void ToString_ShowsNameTypeOrderAndPatience()
    {
        Customer customer = new VipCustomer("Anh Minh", new FakeDish());
        Wait(customer, 5);
        Assert.Equal("Anh Minh (Khách VIP) gọi Món thử - kiên nhẫn 20/25", customer.ToString());
    }

    [Fact]
    public void Factory_ThrowsOnEmptyMenu()
    {
        CustomerFactory factory = new CustomerFactory(new Random(1));
        Assert.Throws<ArgumentException>(() => factory.CreateRandom(new List<Dish>()));
    }

    [Fact]
    public void Factory_CreatesAllThreeTypes_WithOrderFromMenu()
    {
        CustomerFactory factory = new CustomerFactory(new Random(42));
        List<Dish> menu = new List<Dish> { new FakeDish() };
        int normal = 0;
        int vip = 0;
        int picky = 0;
        for (int i = 0; i < 1000; i++)
        {
            Customer customer = factory.CreateRandom(menu);
            Assert.Same(menu[0], customer.Order);
            Assert.False(string.IsNullOrWhiteSpace(customer.Name));
            if (customer is NormalCustomer)
            {
                normal++;
            }
            else if (customer is VipCustomer)
            {
                vip++;
            }
            else if (customer is PickyCustomer)
            {
                picky++;
            }
        }
        // Tỉ lệ 60/20/20, cho phép lệch một chút vì là ngẫu nhiên.
        Assert.InRange(normal, 520, 680);
        Assert.InRange(vip, 130, 270);
        Assert.InRange(picky, 130, 270);
    }
}
