using UnityEngine;
using Zenject;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnOffset = 10f;
    [SerializeField] private float spawnInterval = 2f;
    private PlayerController _player;
    private EnemyPool _enemyPool;
    private GameManager _gameManager;

    [Inject]
    public void Construct(PlayerController player, EnemyPool enemyPool, GameManager gameManager)
    {
        _player = player;
        _enemyPool = enemyPool;
        _gameManager = gameManager;
    }

    private void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), spawnInterval, spawnInterval);
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPosition = _player.transform.position + (Vector3)(Random.insideUnitCircle * spawnOffset);
        spawnPosition.z = 0;

        var enemy = _enemyPool.GetEnemy(spawnPosition);

        if (enemy != null)
        {
            var healthController = enemy.GetComponent<HealthController>();
            if (healthController != null)
            {
                healthController.Construct(_gameManager);
            }

            var enemyBehavior = enemy.GetComponent<Enemy>();
            if (enemyBehavior != null)
            {
                enemyBehavior.Initialize(_player);
            }
        }
    }

}
