using System.Collections.ObjectModel;

namespace OopGame.Core.Menu;

/// <summary>
/// Món Trà Sữa Full Topping Cháy Ví.
/// </summary>
public class TraSua : Drink
{
    private static readonly Dictionary<string, int> _recipe = new Dictionary<string, int>
    {
        { Ingredients.Tea, 1 },
        { Ingredients.Milk, 1 },
        { Ingredients.Pearl, 1 },
        { Ingredients.Ice, 1 }
    };

    public TraSua()
        : base("Trà Sữa Full Topping Cháy Ví", 55000, 2)
    {
    }

    public override string Slogan => "Uống một ly, nhịn ăn ba bữa.";

    public override IReadOnlyDictionary<string, int> GetIngredients()
    {
        return new ReadOnlyDictionary<string, int>(_recipe);
    }
}