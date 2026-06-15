using UnityEngine;

/// <summary>
/// Вешается на тот же GameObject что и WeaponRays.
/// Хранит урон за выстрел — отдельно от bulletForce (физика).
/// Если компонента нет, WeaponRays использует bulletForce как fallback.
/// </summary>
public class WeaponDamage : MonoBehaviour
{
    [Tooltip("Урон за один выстрел")]
    public float damage = 25f;
}
