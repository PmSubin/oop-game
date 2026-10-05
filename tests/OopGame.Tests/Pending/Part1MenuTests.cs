using OopGame.Core;
using OopGame.Core.Menu;

namespace OopGame.Tests;

// Bài kiểm tra cho Phần 1: Thực đơn và món ăn. Không sửa file này.
public class Part1MenuTests
{
    [Theory]
    [InlineData(typeof(Pho), "Phở bò", 45000, 6)]
    [InlineData(typeof(BunBo), "Bún bò", 45000, 6)]
    [InlineData(typeof(ComTam), "Cơm tấm", 40000, 5)]
    [InlineData(typeof(BanhMi), "Bánh mì", 25000, 3)]
    [InlineData(typeof(TraDa), "Trà đá", 5000, 1)]
    public void Dish_HasCorrectNamePriceAndCookTime(Type dishType, string name, int price, int cookTime)
    {
        Dish dish = (Dish)Activator.CreateInstance(dishType)!;
        Assert.Equal(name, dish.Name);
        Assert.Equal(price, dish.Price);
        Assert.Equal(cookTime, dish.CookTimeSeconds);
    }

    [Fact]
    public void MainDishes_InheritMainDish_AndDrinkInheritsDrink()
    {
        Assert.IsAssignableFrom<MainDish>(new Pho());
        Assert.IsAssignableFrom<MainDish>(new BunBo());
        Assert.IsAssignableFrom<MainDish>(new ComTam());
        Assert.IsAssignableFrom<MainDish>(new BanhMi());
        Assert.IsAssignableFrom<Drink>(new TraDa());
        Assert.True(typeof(MainDish).IsAbstract);
        Assert.True(typeof(Drink).IsAbstract);
    }

    [Fact]
    public void Ingredients_MatchRecipeTable()
    {
        AssertRecipe(new Pho(), Ingredients.RiceNoodle, Ingredients.Beef, Ingredients.Vegetables);
        AssertRecipe(new BunBo(), Ingredients.Vermicelli, Ingredients.Beef, Ingredients.Vegetables);
        AssertRecipe(new ComTam(), Ingredients.Rice, Ingredients.Pork, Ingredients.Egg);
        AssertRecipe(new BanhMi(), Ingredients.Bread, Ingredients.Pork, Ingredients.Vegetables);
        AssertRecipe(new TraDa(), Ingredients.Tea, Ingredients.Ice);
    }

    [Fact]
    public void ToString_ShowsCategoryPrefix()
    {
        Assert.Equal("[Món chính] Phở bò - 45.000đ", new Pho().ToString());
        Assert.Equal("[Đồ uống] Trà đá - 5.000đ", new TraDa().ToString());
    }

    [Fact]
    public void MenuBook_HasFiveDishes_AndListIsReadOnly()
    {
        MenuBook menu = new MenuBook();
        Assert.Equal(5, menu.Count);
        Assert.Equal(5, menu.GetAll().Count);
        Assert.False(menu.GetAll() is List<Dish>, "GetAll() phải trả về danh sách chỉ đọc, không trả thẳng List bên trong.");
    }

    [Fact]
    public void MenuBook_FindByName_IgnoresCase_AndReturnsNullWhenMissing()
    {
        MenuBook menu = new MenuBook();
        Assert.IsType<Pho>(menu.FindByName("phở bò"));
        Assert.IsType<TraDa>(menu.FindByName("TRÀ ĐÁ"));
        Assert.Null(menu.FindByName("Pizza"));
    }

    [Fact]
    public void MenuBook_GetRandom_ReturnsDishFromMenu()
    {
        MenuBook menu = new MenuBook();
        Random random = new Random(1);
        for (int i = 0; i < 20; i++)
        {
            Assert.Contains(menu.GetRandom(random), menu.GetAll());
        }
    }

    private static void AssertRecipe(Dish dish, params string[] expected)
    {
        IReadOnlyDictionary<string, int> recipe = dish.GetIngredients();
        Assert.Equal(expected.Length, recipe.Count);
        foreach (string ingredient in expected)
        {
            Assert.Equal(1, recipe[ingredient]);
        }
    }
}
