using System.Collections.ObjectModel;
using OopGame.Core.Menu;

namespace OopGame.Core.Cooking;

// kho nguyen lieu cua quan: luu so luong tung nguyen lieu
public class Inventory
{
    private readonly Dictionary<string, int> _stock = new Dictionary<string, int>();

    // so luong hien co cua mot nguyen lieu
    public int GetAmount(string ingredient)
    {
        int amount;
        if (_stock.TryGetValue(ingredient, out amount))
        {
            return amount;
        }
        return 0;
    }

    // them nguyen lieu vao kho (cong don vao so dang co)
    public void Add(string ingredient, int amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Số lượng phải lớn hơn 0.");
        }

        _stock[ingredient] = GetAmount(ingredient) + amount;
    }

    // kiem tra kho co du nguyen lieu de nau mot phan mon nay khong
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

    // tru nguyen lieu de nau mon
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

    // toan bo kho duoi dang chi doc (ten nguyen lieu va so luong)
    public IReadOnlyDictionary<string, int> GetAll()
    {
        return new ReadOnlyDictionary<string, int>(_stock);
    }
}