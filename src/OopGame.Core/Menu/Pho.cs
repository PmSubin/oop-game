using System.Collections.ObjectModel;

namespace OopGame.Core.Menu;

// mon pho go deadline
public class Pho : MainDish
{
    private static readonly Dictionary<string, int> _recipe = new Dictionary<string, int>
    {
        { Ingredients.RiceNoodle, 1 },
        { Ingredients.Beef, 1 },
        { Ingredients.Vegetables, 1 }
    };

    // khoi tao mon pho
    public Pho()
        : base("Phở Gõ Deadline", 45000, 6)
    {
    }

    // khau hieu cua mon
    public override string Slogan => "Ăn xong chạy deadline xuyên đêm.";

    // cong thuc nguyen lieu
    public override IReadOnlyDictionary<string, int> GetIngredients()
    {
        return new ReadOnlyDictionary<string, int>(_recipe);
    }
}