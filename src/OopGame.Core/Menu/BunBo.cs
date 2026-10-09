using System.Collections.ObjectModel;

namespace OopGame.Core.Menu;

// mon bun bo cay nhu nguoi yeu cu
public class BunBo : MainDish
{
    private static readonly Dictionary<string, int> _recipe = new Dictionary<string, int>
    {
        { Ingredients.Vermicelli, 1 },
        { Ingredients.Beef, 1 },
        { Ingredients.Vegetables, 1 }
    };

    // khoi tao mon bun bo
    public BunBo()
        : base("Bún Bò Cay Như Người Yêu Cũ", 45000, 6)
    {
    }

    // khau hieu cua mon
    public override string Slogan => "Cay xé lưỡi, nhớ mãi không quên.";

    // cong thuc nguyen lieu
    public override IReadOnlyDictionary<string, int> GetIngredients()
    {
        return new ReadOnlyDictionary<string, int>(_recipe);
    }
}