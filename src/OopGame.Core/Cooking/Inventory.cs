using System.Collections.ObjectModel;
using OopGame.Core.Menu;

namespace OopGame.Core.Cooking;

/// <summary>
/// Kho nguyên liệu của quán: lưu số lượng từng nguyên liệu.
/// Bên ngoài chỉ thao tác qua phương thức, không sửa thẳng số lượng được.
/// </summary>
public class Inventory
{
    private readonly Dictionary<string, int> _stock = new Dictionary<string, int>();

    /// <summary>
    /// Số lượng hiện có của một nguyên liệu. Chưa từng thêm thì trả về 0.
    /// </summary>
    public int GetAmount(string ingredient)
    {
        int amount;
        if (_stock.TryGetValue(ingredient, out amount))
        {
            return amount;
        }
        return 0;
    }

    /// <summary>
    /// Thêm nguyên liệu vào kho (cộng dồn vào số đang có).
    /// </summary>
    public void Add(string ingredient, int amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Số lượng phải lớn hơn 0.");
        }

        _stock[ingredient] = GetAmount(ingredient) + amount;
    }

    /// <summary>
    /// Kiểm tra kho có đủ nguyên liệu để nấu một phần món này không.
    /// </summary>
    public bool HasIngredients(Dish dish)
    {
        foreach (KeyValuePair<string, int> item in dish.GetIngredients())
        {
            if (GetAmount(item.Key) < item.Value)
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Trừ nguyên liệu để nấu món. Thiếu bất kỳ thứ nào thì trả về false và không trừ gì cả.
    /// </summary>
    public bool TryConsume(Dish dish)
    {
        if (!HasIngredients(dish))
        {
            return false;
        }

        foreach (KeyValuePair<string, int> item in dish.GetIngredients())
        {
            _stock[item.Key] = GetAmount(item.Key) - item.Value;
        }
        return true;
    }

    /// <summary>
    /// Toàn bộ kho dưới dạng chỉ đọc (tên nguyên liệu và số lượng).
    /// </summary>
    public IReadOnlyDictionary<string, int> GetAll()
    {
        return new ReadOnlyDictionary<string, int>(_stock);
    }
}