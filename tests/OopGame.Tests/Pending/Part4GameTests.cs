using OopGame.Core;
using OopGame.Core.Customers;
using OopGame.Core.Game;

namespace OopGame.Tests;

// Bài kiểm tra cho Phần 4: Quán và ngày làm việc. Cần Phần 1, 2, 3 đã xong. Không sửa file này.
public class Part4GameTests
{
    private static void TickMany(Restaurant restaurant, int count)
    {
        for (int i = 0; i < count; i++)
        {
            restaurant.Tick();
        }
    }

    [Fact]
    public void NewRestaurant_HasStartingValues()
    {
        Restaurant restaurant = new Restaurant(new Random(1));
        Assert.Equal(200000, restaurant.Money);
        Assert.Equal(100, restaurant.Reputation);
        Assert.Equal(0, restaurant.Day);
        Assert.False(restaurant.IsOpen);
        Assert.False(restaurant.IsGameOver);
        Assert.Equal("08:00", restaurant.ClockText);
        Assert.Equal(5, restaurant.Menu.Count);
        Assert.Equal(5, restaurant.Inventory.GetAmount(Ingredients.Beef));
        Assert.Empty(restaurant.WaitingCustomers);
        Assert.Null(restaurant.CurrentSummary);
    }

    [Fact]
    public void OpenDay_StartsDayOne_AndCannotOpenTwice()
    {
        Restaurant restaurant = new Restaurant(new Random(1));
        Assert.True(restaurant.OpenDay());
        Assert.Equal(1, restaurant.Day);
        Assert.True(restaurant.IsOpen);
        Assert.Equal("08:00", restaurant.ClockText);
        Assert.Equal(1, restaurant.CurrentSummary!.Day);
        Assert.False(restaurant.OpenDay());
    }

    [Fact]
    public void Tick_WhenClosed_DoesNothing()
    {
        Restaurant restaurant = new Restaurant(new Random(1));
        Assert.Empty(restaurant.Tick());
        Assert.Equal("08:00", restaurant.ClockText);
    }

    [Fact]
    public void Tick_AdvancesTenMinutes_AndClosesAt20h()
    {
        Restaurant restaurant = new Restaurant(new Random(1));
        restaurant.OpenDay();
        restaurant.Tick();
        Assert.Equal("08:10", restaurant.ClockText);
        TickMany(restaurant, 70);
        Assert.Equal("19:50", restaurant.ClockText);
        Assert.True(restaurant.IsOpen);
        restaurant.Tick();
        Assert.Equal("20:00", restaurant.ClockText);
        Assert.False(restaurant.IsOpen);
        Assert.Empty(restaurant.WaitingCustomers);
    }

    [Fact]
    public void Customers_ArriveButNeverMoreThanFive()
    {
        Restaurant restaurant = new Restaurant(new Random(7));
        restaurant.OpenDay();
        int maxSeen = 0;
        for (int i = 0; i < 60; i++)
        {
            restaurant.Tick();
            maxSeen = Math.Max(maxSeen, restaurant.WaitingCustomers.Count);
        }
        Assert.InRange(maxSeen, 1, 5);
    }

    [Fact]
    public void CustomersLeaving_LowerReputation()
    {
        Restaurant restaurant = new Restaurant(new Random(3));
        restaurant.OpenDay();
        TickMany(restaurant, 72);
        Assert.True(restaurant.CurrentSummary!.CustomersLeft > 0);
        Assert.Equal(100 - 10 * restaurant.CurrentSummary.CustomersLeft, restaurant.Reputation);
    }

    [Fact]
    public void BuyIngredient_SpendsMoneyAndAddsStock()
    {
        Restaurant restaurant = new Restaurant(new Random(1));
        restaurant.OpenDay();
        Assert.True(restaurant.BuyIngredient(Ingredients.Beef, 2));
        Assert.Equal(170000, restaurant.Money);
        Assert.Equal(7, restaurant.Inventory.GetAmount(Ingredients.Beef));
        Assert.Equal(30000, restaurant.CurrentSummary!.Expenses);
    }

    [Fact]
    public void BuyIngredient_FailsWhenInvalidOrTooExpensive()
    {
        Restaurant restaurant = new Restaurant(new Random(1));
        Assert.False(restaurant.BuyIngredient(Ingredients.Beef, 0));
        Assert.False(restaurant.BuyIngredient("Tôm hùm", 1));
        Assert.False(restaurant.BuyIngredient(Ingredients.Beef, 100));
        Assert.Equal(200000, restaurant.Money);
    }

    [Fact]
    public void Serve_PaysPriceAndTip_AndRemovesCustomer()
    {
        Restaurant restaurant = new Restaurant(new Random(5));
        restaurant.OpenDay();
        while (restaurant.WaitingCustomers.Count == 0)
        {
            restaurant.Tick();
        }
        Customer customer = restaurant.WaitingCustomers[0];
        Assert.True(restaurant.StartCooking(customer.Order));
        while (restaurant.Kitchen.ReadyDishes.Count == 0)
        {
            restaurant.Tick();
        }
        int tip = customer.CalculateTip();

        Assert.True(restaurant.Serve(customer));
        Assert.Equal(200000 + customer.Order.Price + tip, restaurant.Money);
        Assert.DoesNotContain(customer, restaurant.WaitingCustomers);
        Assert.Equal(1, restaurant.CurrentSummary!.CustomersServed);
        Assert.False(restaurant.Serve(customer));
    }

    [Fact]
    public void Serve_FailsWhenDishNotReady()
    {
        Restaurant restaurant = new Restaurant(new Random(5));
        restaurant.OpenDay();
        while (restaurant.WaitingCustomers.Count == 0)
        {
            restaurant.Tick();
        }
        Assert.False(restaurant.Serve(restaurant.WaitingCustomers[0]));
        Assert.Equal(200000, restaurant.Money);
    }

    [Fact]
    public void DaySummary_ComputesProfit_AndFormatsText()
    {
        DaySummary summary = new DaySummary(2);
        summary.RecordServed(150000);
        summary.RecordServed(50000);
        summary.RecordLeft();
        summary.RecordExpense(30000);
        Assert.Equal(2, summary.CustomersServed);
        Assert.Equal(1, summary.CustomersLeft);
        Assert.Equal(170000, summary.Profit);
        Assert.Equal(
            "Tổng kết ngày 2\nĐã phục vụ: 2 khách\nKhách bỏ về: 1 khách\nDoanh thu: 200.000đ\nChi phí: 30.000đ\nLợi nhuận: 170.000đ",
            summary.ToString());
    }
}
