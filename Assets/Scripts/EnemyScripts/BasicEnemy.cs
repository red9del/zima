using System;
using UnityEngine;

public class BasicEnemy : MonoBehaviour, IDamageable
{
    // Срабатывает при смерти — EnemyRespawn и другие компоненты подписываются сюда
    public event Action OnDeath;

    // Множитель отбрасывания при получении урона (настраивается в инспекторе от 3 до 5)
    [Range(3f, 5f)]
    public float knockbackMultiplier = 3f;

    // Максимальное количество HP врага (настраивается в инспекторе от 50 до 100)
    [Range(50, 100)]
    public int maxHP = 50;

    // Текущее HP врага, уменьшается при получении урона
    private int currentHP;

    // Ссылка на Rigidbody — нужна для управления физикой при смерти
    private Rigidbody rb;

    // Направление последнего выстрела — чтобы тело отлетело в нужную сторону
    private Vector3 lastHitDirection = Vector3.back;

    public bool IsAlive => currentHP > 0;

    void Start()
    {
        // Инициализируем текущее HP равным максимальному
        currentHP = maxHP;

        // Получаем Rigidbody и сразу включаем Kinematic, чтобы физика не мешала движению врага
        rb = GetComponent<Rigidbody>();
        if (rb != null)
            rb.isKinematic = true;
    }

    // Вызывается из WeaponRays перед TakeDamage — передаёт направление выстрела
    public void SetHitDirection(Vector3 direction)
    {
        lastHitDirection = direction.normalized;
    }

    // Реализация IDamageable
    public void TakeDamage(float amount)
    {
        if (!IsAlive) return;

        currentHP -= (int)amount;

        if (currentHP <= 0)
            Die();
    }

    // Обрабатывает смерть: тело падает и отлетает в сторону выстрела
    private void Die()
    {
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.AddForce(lastHitDirection * knockbackMultiplier, ForceMode.Impulse);
        }

        OnDeath?.Invoke();
    }

    // Сбрасывает врага в живое состояние — вызывается из EnemyRespawn
    public void Respawn()
    {
        currentHP = maxHP;
        lastHitDirection = Vector3.back;

        if (rb != null)
        {
            rb.linearVelocity  = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic     = true;
        }
    }
}
