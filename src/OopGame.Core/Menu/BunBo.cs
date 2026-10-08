using System.Collections.ObjectModel;

namespace OopGame.Core.Menu;

/// <summary>
/// Món Bún Bò Cay Như Người Yêu Cũ.
/// </summary>
public class BunBo : MainDish
{
    private static readonly Dictionary<string, int> _recipe = new Dictionary<string, int>
    {
        { Ingredients.Vermicelli, 1 },
        { Ingredients.Beef, 1 },
        { Ingredients.Vegetables, 1 }
    };

    /// <summary>
    /// Khởi tạo món bún bò.
    /// </summary>
    public BunBo()
        : base("Bún Bò Cay Như Người Yêu Cũ", 45000, 6)
    {
    }

    /// <summary>
    /// Khẩu hiệu của món.
    /// </summary>
    public override string Slogan => "Cay xé lưỡi, nhớ mãi không quên.";

    /// <summary>
    /// Công thức nguyên liệu.
    /// </summary>
    public override IReadOnlyDictionary<string, int> GetIngredients()
    {
        return new ReadOnlyDictionary<string, int>(_recipe);
    }
}