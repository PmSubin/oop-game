using System.Collections.ObjectModel;

namespace OopGame.Core.Menu;

/// <summary>
/// Món Bánh Mì Không Người Yêu.
/// </summary>
public class BanhMi : MainDish
{
    private static readonly Dictionary<string, int> _recipe = new Dictionary<string, int>
    {
        { Ingredients.Bread, 1 },
        { Ingredients.Pork, 1 },
        { Ingredients.Vegetables, 1 }
    };

    public BanhMi()
        : base("Bánh Mì Không Người Yêu", 25000, 3)
    {
    }

    public override string Slogan => "Có thịt, có rau, chỉ thiếu người yêu.";

    public override IReadOnlyDictionary<string, int> GetIngredients()
    {
        return new ReadOnlyDictionary<string, int>(_recipe);
    }
}