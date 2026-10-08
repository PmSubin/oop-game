using System.Collections.ObjectModel;

namespace OopGame.Core.Menu;

/// <summary>
/// Món Mì Tôm Trứng Mùa Thi.
/// </summary>
public class MiTom : MainDish
{
    private static readonly Dictionary<string, int> _recipe = new Dictionary<string, int>
    {
        { Ingredients.InstantNoodle, 1 },
        { Ingredients.Egg, 1 }
    };

    public MiTom()
        : base("Mì Tôm Trứng Mùa Thi", 20000, 2)
    {
    }

    public override string Slogan => "Món ăn quốc dân của sinh viên ôn thi.";

    public override IReadOnlyDictionary<string, int> GetIngredients()
    {
        return new ReadOnlyDictionary<string, int>(_recipe);
    }
}