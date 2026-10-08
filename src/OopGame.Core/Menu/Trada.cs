using System.Collections.ObjectModel;

namespace OopGame.Core.Menu;

/// <summary>
/// Món Trà Đá Chém Gió.
/// </summary>
public class TraDa : Drink
{
    private static readonly Dictionary<string, int> _recipe = new Dictionary<string, int>
    {
        { Ingredients.Tea, 1 },
        { Ingredients.Ice, 1 }
    };

    public TraDa()
        : base("Trà Đá Chém Gió", 5000, 1)
    {
    }

    public override string Slogan => "Một ly trà, ba tiếng chém gió.";

    public override IReadOnlyDictionary<string, int> GetIngredients()
    {
        return new ReadOnlyDictionary<string, int>(_recipe);
    }
}