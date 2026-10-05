using OopGame.Core;
using OopGame.Core.Menu;

namespace OopGame.Tests;

// Kiểm tra các file dùng chung đã có sẵn (Dish, Ingredients). Không cần sửa file này.
public class SharedFilesTests
{
    // Món giả chỉ dùng trong file kiểm tra này.
    private sealed class FakeDish : Dish
    {
        public FakeDish(int price, int cookTime) : base("Món thử", price, cookTime) { }

        public override IReadOnlyDictionary<string, int> GetIngredients()
        {
            return new Dictionary<string, int> { [Ingredients.Tea] = 1 };
        }
    }

    [Fact]
    public void Dish_ToString_ShowsVietnamesePrice()
    {
        Assert.Equal("Món thử - 45.000đ", new FakeDish(45000, 3).ToString());
    }

    [Fact]
    public void Dish_RejectsZeroPriceOrCookTime()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new FakeDish(0, 3));
        Assert.Throws<ArgumentOutOfRangeException>(() => new FakeDish(1000, 0));
    }

    [Fact]
    public void Ingredients_All_HasTenDistinctNames()
    {
        Assert.Equal(10, Ingredients.All.Count);
        Assert.Equal(10, Ingredients.All.Distinct().Count());
    }
}
