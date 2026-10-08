using System.Collections.ObjectModel;

namespace OopGame.Core.Menu;

/// <summary>
/// Món Phở Gõ Deadline.
/// </summary>
public class Pho : MainDish
{
    private static readonly Dictionary<string, int> _recipe = new Dictionary<string, int>
    {
        { Ingredients.RiceNoodle, 1 },
        { Ingredients.Beef, 1 },
        { Ingredients.Vegetables, 1 }
    };

    /// <summary>
    /// Khởi tạo món phở.
    /// </summary>
    public Pho()
        : base("Phở Gõ Deadline", 45000, 6)
    {
    }

    /// <summary>
    /// Khẩu hiệu của món.
    /// </summary>
    public override string Slogan => "Ăn xong chạy deadline xuyên đêm.";

    /// <summary>
    /// Công thức nguyên liệu.
    /// </summary>
    public override IReadOnlyDictionary<string, int> GetIngredients()
    {
        return new ReadOnlyDictionary<string, int>(_recipe);
    }
}