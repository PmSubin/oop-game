using OopGame.Core.Menu;

namespace OopGame.Core.Cooking;

// mot mon dang nau trong bep: nho mon nao va con bao nhieu giay nua thi xong
public class CookingOrder
{
    // bat dau nau mot mon
    public CookingOrder(Dish dish)
    {
        Dish = dish;
        RemainingSeconds = dish.CookTimeSeconds;
    }

    // mon dang duoc nau
    public Dish Dish { get; }

    // so giay con lai de nau xong
    public int RemainingSeconds { get; private set; }

    // mon da nau xong chua (het thoi gian cho)
    public bool IsDone => RemainingSeconds == 0;

    // troi qua mot so giay
    public void Advance(int seconds)
    {
        RemainingSeconds = Math.Max(0, RemainingSeconds - seconds);
    }

    // mo ta mon dang nau, vi du: "pho go deadline - con 3 giay"
    public override string ToString()
    {
        return $"{Dish.Name} - còn {RemainingSeconds} giây";
    }
}