using System.Collections.ObjectModel;

namespace OopGame.Core.Menu;

/// <summary>
/// Món Cơm Tấm Cứu Đói Cuối Tháng.
/// </summary>
public class ComTam : MainDish
{
    private static readonly Dictionary<string, int> _recipe = new Dictionary<string, int>
    {
        { Ingredients.Rice, 1 },
        { Ingredients.Pork, 1 },
        { Ingredients.Egg, 1 }
    };

    public ComTam()
        : base("Cơm Tấm Cứu Đói Cuối Tháng", 40000, 5)
    {
    }

    public override string Slogan => "Ví mỏng nhưng bụng vẫn phải no.";

    public override IReadOnlyDictionary<string, int> GetIngredients()
    {
        return new ReadOnlyDictionary<string, int>(_recipe);
    }
}