using System.Collections;
using UnityEngine;

/// <summary>
/// Воскрешает врага на исходной позиции через заданное время после смерти.
/// Костяк механики — будет дополняться.
///
/// Использование: повесить на тот же GameObject что и BasicEnemy.
/// </summary>
[RequireComponent(typeof(BasicEnemy))]
public class EnemyRespawn : MonoBehaviour
{
    [Tooltip("Время до воскрешения в секундах")]
    public float respawnDelay = 5f;

    // Исходные позиция и поворот — запоминаются при старте сцены
    private Vector3 spawnPosition;
    private Quaternion spawnRotation;

    private BasicEnemy enemy;

    private void Awake()
    {
        enemy = GetComponent<BasicEnemy>();

        // Запоминаем позицию из сцены до того, как враг куда-то упадёт
        spawnPosition = transform.position;
        spawnRotation = transform.rotation;

        // Подписываемся на смерть
        enemy.OnDeath += HandleDeath;
    }

    private void OnDestroy()
    {
        enemy.OnDeath -= HandleDeath;
    }

    private void HandleDeath()
    {
        StartCoroutine(RespawnRoutine());
    }

    private IEnumerator RespawnRoutine()
    {
        yield return new WaitForSeconds(respawnDelay);

        // Возвращаем на исходную позицию и поворот
        transform.SetPositionAndRotation(spawnPosition, spawnRotation);

        // Сбрасываем состояние врага
        enemy.Respawn();
    }
}
