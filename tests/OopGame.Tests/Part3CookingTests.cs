using OopGame.Core;
using OopGame.Core.Cooking;
using OopGame.Core.Menu;

namespace OopGame.Tests;

// Bài kiểm tra cho Phần 3: Bếp và kho nguyên liệu. Không sửa file này.
public class Part3CookingTests
{
    // Món giả, không phụ thuộc Phần 1: cần 1 thịt bò + 2 rau.
    private sealed class FakeDish : Dish
    {
        public FakeDish(string name = "Món thử", int cookTime = 3) : base(name, 30000, cookTime) { }

        public override string Slogan => "Slogan thử";

        public override IReadOnlyDictionary<string, int> GetIngredients()
        {
            return new Dictionary<string, int> { [Ingredients.Beef] = 1, [Ingredients.Vegetables] = 2 };
        }
    }

    private static Inventory FullInventory()
    {
        Inventory inventory = new Inventory();
        foreach (string ingredient in Ingredients.All)
        {
            inventory.Add(ingredient, 10);
        }
        return inventory;
    }

    [Fact]
    public void Inventory_StartsEmpty_AndAddAccumulates()
    {
        Inventory inventory = new Inventory();
        Assert.Equal(0, inventory.GetAmount(Ingredients.Beef));
        inventory.Add(Ingredients.Beef, 3);
        inventory.Add(Ingredients.Beef, 2);
        Assert.Equal(5, inventory.GetAmount(Ingredients.Beef));
        Assert.Equal(5, inventory.GetAll()[Ingredients.Beef]);
    }

    [Fact]
    public void Inventory_Add_RejectsZeroOrNegative()
    {
        Inventory inventory = new Inventory();
        Assert.Throws<ArgumentOutOfRangeException>(() => inventory.Add(Ingredients.Beef, 0));
        Assert.Throws<ArgumentOutOfRangeException>(() => inventory.Add(Ingredients.Beef, -1));
    }

    [Fact]
    public void Inventory_TryConsume_TakesAllOrNothing()
    {
        Inventory inventory = new Inventory();
        inventory.Add(Ingredients.Beef, 1);
        inventory.Add(Ingredients.Vegetables, 1);

        // Thiếu 1 rau: không được trừ gì cả.
        Assert.False(inventory.HasIngredients(new FakeDish()));
        Assert.False(inventory.TryConsume(new FakeDish()));
        Assert.Equal(1, inventory.GetAmount(Ingredients.Beef));
        Assert.Equal(1, inventory.GetAmount(Ingredients.Vegetables));

        inventory.Add(Ingredients.Vegetables, 1);
        Assert.True(inventory.TryConsume(new FakeDish()));
        Assert.Equal(0, inventory.GetAmount(Ingredients.Beef));
        Assert.Equal(0, inventory.GetAmount(Ingredients.Vegetables));
    }

    [Fact]
    public void Supplier_HasPriceTable()
    {
        Supplier supplier = new Supplier();
        Assert.Equal(5000, supplier.GetPrice(Ingredients.RiceNoodle));
        Assert.Equal(15000, supplier.GetPrice(Ingredients.Beef));
        Assert.Equal(10000, supplier.GetPrice(Ingredients.Pork));
        Assert.Equal(500, supplier.GetPrice(Ingredients.Ice));
        Assert.Equal(30000, supplier.GetTotalPrice(Ingredients.Pork, 3));
        Assert.Equal(13, supplier.GetAvailableIngredients().Count);
        Assert.Equal(4000, supplier.GetPrice(Ingredients.InstantNoodle));
        Assert.Equal(5000, supplier.GetPrice(Ingredients.Milk));
        Assert.Equal(6000, supplier.GetPrice(Ingredients.Pearl));
        Assert.Throws<ArgumentException>(() => supplier.GetPrice("Tôm hùm"));
    }

    [Fact]
    public void CookingOrder_CountsDownToZero()
    {
        CookingOrder order = new CookingOrder(new FakeDish(cookTime: 3));
        Assert.Equal(3, order.RemainingSeconds);
        Assert.Equal("Món thử - còn 3 giây", order.ToString());
        order.Advance(2);
        Assert.False(order.IsDone);
        order.Advance(5);
        Assert.Equal(0, order.RemainingSeconds);
        Assert.True(order.IsDone);
    }

    [Fact]
    public void Kitchen_ImplementsIUpdatable_AndHasTwoSlots()
    {
        Assert.True(typeof(IUpdatable).IsAssignableFrom(typeof(Kitchen)));
        Assert.Equal(2, Kitchen.MaxSlots);
    }

    [Fact]
    public void Kitchen_CannotCookWithoutIngredients()
    {
        Kitchen kitchen = new Kitchen(new Inventory());
        Assert.False(kitchen.StartCooking(new FakeDish()));
        Assert.Empty(kitchen.CookingOrders);
    }

    [Fact]
    public void Kitchen_CooksAtMostTwoDishes()
    {
        Kitchen kitchen = new Kitchen(FullInventory());
        Assert.True(kitchen.StartCooking(new FakeDish()));
        Assert.True(kitchen.StartCooking(new FakeDish()));
        Assert.True(kitchen.IsFull);
        Assert.False(kitchen.StartCooking(new FakeDish()));
        Assert.Equal(2, kitchen.CookingOrders.Count);
    }

    [Fact]
    public void Kitchen_WhenFull_DoesNotConsumeIngredients()
    {
        Inventory inventory = FullInventory();
        Kitchen kitchen = new Kitchen(inventory);
        kitchen.StartCooking(new FakeDish());
        kitchen.StartCooking(new FakeDish());
        kitchen.StartCooking(new FakeDish());
        Assert.Equal(8, inventory.GetAmount(Ingredients.Beef));
    }

    [Fact]
    public void Kitchen_MovesFinishedDishToReady()
    {
        Kitchen kitchen = new Kitchen(FullInventory());
        kitchen.StartCooking(new FakeDish("Món nhanh", 1));
        kitchen.StartCooking(new FakeDish("Món chậm", 3));

        kitchen.Update(1);
        Assert.Single(kitchen.ReadyDishes);
        Assert.Equal("Món nhanh", kitchen.ReadyDishes[0].Name);
        Assert.Single(kitchen.CookingOrders);
        Assert.False(kitchen.IsFull);

        kitchen.Update(1);
        kitchen.Update(1);
        Assert.Equal(2, kitchen.ReadyDishes.Count);
        Assert.Empty(kitchen.CookingOrders);
    }

    [Fact]
    public void Kitchen_TakeReadyDish_MatchesByName()
    {
        Kitchen kitchen = new Kitchen(FullInventory());
        kitchen.StartCooking(new FakeDish("Phở bò", 1));
        kitchen.Update(1);

        Assert.False(kitchen.TakeReadyDish(new FakeDish("Bánh mì")));
        Assert.True(kitchen.TakeReadyDish(new FakeDish("Phở bò")));
        Assert.Empty(kitchen.ReadyDishes);
        Assert.False(kitchen.TakeReadyDish(new FakeDish("Phở bò")));
    }

    [Fact]
    public void Kitchen_ListsAreReadOnly()
    {
        Kitchen kitchen = new Kitchen(FullInventory());
        Assert.False(kitchen.CookingOrders is List<CookingOrder>, "CookingOrders phải là danh sách chỉ đọc.");
        Assert.False(kitchen.ReadyDishes is List<Dish>, "ReadyDishes phải là danh sách chỉ đọc.");
    }
}
