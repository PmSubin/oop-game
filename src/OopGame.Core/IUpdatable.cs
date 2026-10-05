namespace OopGame.Core;

/// <summary>
/// Những thứ thay đổi theo thời gian khi quán đang mở (khách, bếp).
/// Quán gọi Update(1) mỗi giây.
/// </summary>
public interface IUpdatable
{
    void Update(int elapsedSeconds);
}
