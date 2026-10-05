using OopGame.Core;
using OopGame.Core.Menu;

namespace OopGame.Tests;

// Bài kiểm tra cho Phần 1: Thực đơn và món ăn. Không sửa file này.
public class Part1MenuTests
{
    [Theory]
    [InlineData(typeof(Pho), "Phở Gõ Deadline", 45000, 6)]
    [InlineData(typeof(BunBo), "Bún Bò Cay Như Người Yêu Cũ", 45000, 6)]
    [InlineData(typeof(ComTam), "Cơm Tấm Cứu Đói Cuối Tháng", 40000, 5)]
    [InlineData(typeof(BanhMi), "Bánh Mì Không Người Yêu", 25000, 3)]
    [InlineData(typeof(MiTom), "Mì Tôm Trứng Mùa Thi", 20000, 2)]
    [InlineData(typeof(TraDa), "Trà Đá Chém Gió", 5000, 1)]
    [InlineData(typeof(TraSua), "Trà Sữa Full Topping Cháy Ví", 55000, 2)]
    public void Dish_HasCorrectNamePriceAndCookTime(Type dishType, string name, int price, int cookTime)
    {
        Dish dish = (Dish)Activator.CreateInstance(dishType)!;
        Assert.Equal(name, dish.Name);
        Assert.Equal(price, dish.Price);
        Assert.Equal(cookTime, dish.CookTimeSeconds);
    }

    [Theory]
    [InlineData(typeof(Pho), "Ăn xong chạy deadline xuyên đêm.")]
    [InlineData(typeof(BunBo), "Cay xé lưỡi, nhớ mãi không quên.")]
    [InlineData(typeof(ComTam), "Ví mỏng nhưng bụng vẫn phải no.")]
    [InlineData(typeof(BanhMi), "Có thịt, có rau, chỉ thiếu người yêu.")]
    [InlineData(typeof(MiTom), "Món ăn quốc dân của sinh viên ôn thi.")]
    [InlineData(typeof(TraDa), "Một ly trà, ba tiếng chém gió.")]
    [InlineData(typeof(TraSua), "Uống một ly, nhịn ăn ba bữa.")]
    public void Dish_HasCorrectSlogan(Type dishType, string slogan)
    {
        Dish dish = (Dish)Activator.CreateInstance(dishType)!;
        Assert.Equal(slogan, dish.Slogan);
    }

    [Fact]
    public void MainDishes_InheritMainDish_AndDrinksInheritDrink()
    {
        Assert.IsAssignableFrom<MainDish>(new Pho());
        Assert.IsAssignableFrom<MainDish>(new BunBo());
        Assert.IsAssignableFrom<MainDish>(new ComTam());
        Assert.IsAssignableFrom<MainDish>(new BanhMi());
        Assert.IsAssignableFrom<MainDish>(new MiTom());
        Assert.IsAssignableFrom<Drink>(new TraDa());
        Assert.IsAssignableFrom<Drink>(new TraSua());
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
        AssertRecipe(new MiTom(), Ingredients.InstantNoodle, Ingredients.Egg);
        AssertRecipe(new TraDa(), Ingredients.Tea, Ingredients.Ice);
        AssertRecipe(new TraSua(), Ingredients.Tea, Ingredients.Milk, Ingredients.Pearl, Ingredients.Ice);
    }

    [Fact]
    public void Recipe_CannotBeChangedFromOutside()
    {
        IReadOnlyDictionary<string, int> recipe = new Pho().GetIngredients();
        Assert.False(recipe is Dictionary<string, int>, "GetIngredients() phải trả về bản chỉ đọc, không trả thẳng Dictionary bên trong.");
    }

    [Fact]
    public void ToString_ShowsCategoryPrefix()
    {
        Assert.Equal("[Món chính] Phở Gõ Deadline - 45.000đ", new Pho().ToString());
        Assert.Equal("[Đồ uống] Trà Sữa Full Topping Cháy Ví - 55.000đ", new TraSua().ToString());
    }

    [Fact]
    public void MenuBook_HasSevenDishes_AndListIsReadOnly()
    {
        MenuBook menu = new MenuBook();
        Assert.Equal(7, menu.Count);
        Assert.Equal(7, menu.GetAll().Count);
        Assert.False(menu.GetAll() is List<Dish>, "GetAll() phải trả về danh sách chỉ đọc, không trả thẳng List bên trong.");
    }

    [Fact]
    public void MenuBook_FindByName_IgnoresCase_AndReturnsNullWhenMissing()
    {
        MenuBook menu = new MenuBook();
        Assert.IsType<Pho>(menu.FindByName("phở gõ deadline"));
        Assert.IsType<TraDa>(menu.FindByName("TRÀ ĐÁ CHÉM GIÓ"));
        Assert.Null(menu.FindByName("Pizza Dứa"));
    }

    [Fact]
    public void MenuBook_GetRandom_ReturnsDishFromMenu()
    {
        MenuBook menu = new MenuBook();
        Random random = new Random(1);
        for (int i = 0; i < 30; i++)
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
