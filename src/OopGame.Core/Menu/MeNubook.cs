namespace OopGame.Core.Menu;

// thuc don cua quan
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

    // so mon trong thuc don
    public int Count => _dishes.Count;

    // lay toan bo mon
    public IReadOnlyList<Dish> GetAll()
    {
        return _dishes.AsReadOnly();
    }

    // lay ngau nhien mot mon
    public Dish GetRandom(Random random)
    {
        return _dishes[random.Next(_dishes.Count)];
    }

    // tim mon theo ten
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