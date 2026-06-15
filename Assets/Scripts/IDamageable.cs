/// <summary>
/// Интерфейс для всего, что принимает урон: враги, игрок, объекты.
/// </summary>
public interface IDamageable
{
    void TakeDamage(float amount);
    bool IsAlive { get; }
}
