namespace OopGame.Core.Menu;

/// <summary>
/// Thực đơn của quán.
/// </summary>
public class MenuBook
{
    private readonly List<Dish> _dishes;

    public MenuBook()
    {
        _dishes = new List<Dish>
        {
            new Pho(),
            new BunBo(),
            new ComTam(),
            new BanhMi(),
            new MiTom(),
            new TraDa(),
            new TraSua()
        };
    }

    /// <summary>
    /// Số món trong thực đơn.
    /// </summary>
    public int Count => _dishes.Count;

    /// <summary>
    /// Lấy toàn bộ món.
    /// </summary>
    public IReadOnlyList<Dish> GetAll()
    {
        return _dishes.AsReadOnly();
    }

    /// <summary>
    /// Lấy ngẫu nhiên một món.
    /// </summary>
    public Dish GetRandom(Random random)
    {
        return _dishes[random.Next(_dishes.Count)];
    }

    /// <summary>
    /// Tìm món theo tên.
    /// </summary>
    public Dish? FindByName(string name)
    {
        foreach (Dish dish in _dishes)
        {
            if (string.Equals(
                dish.Name,
                name,
                StringComparison.CurrentCultureIgnoreCase))
            {
                return dish;
            }
        }

        return null;
    }
}