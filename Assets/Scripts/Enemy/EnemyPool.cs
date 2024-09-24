using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyPool : IInitializable
{
    private readonly GameObject[] _enemyPrefabs;
    private readonly List<HealthController> _enemies = new List<HealthController>();
    private readonly int _initialPoolSize;

    public EnemyPool(GameObject[] enemyPrefabs, int initialPoolSize)
    {
        _enemyPrefabs = enemyPrefabs;
        _initialPoolSize = initialPoolSize;
    }

    public void Initialize()
    {
        for (int i = 0; i < _initialPoolSize; i++)
        {
            foreach (var prefab in _enemyPrefabs)
            {
                var enemy = Object.Instantiate(prefab);
                enemy.SetActive(false);
                _enemies.Add(enemy.GetComponent<HealthController>());
            }
        }
    }

    public HealthController GetEnemy(Vector3 position)
    {
        foreach (var enemy in _enemies)
        {
            if (!enemy.gameObject.activeInHierarchy)
            {
                enemy.transform.position = position;
                enemy.gameObject.SetActive(true);
                return enemy;
            }
        }

        var randomPrefab = _enemyPrefabs[Random.Range(0, _enemyPrefabs.Length)];
        var newEnemy = Object.Instantiate(randomPrefab).GetComponent<HealthController>();
        newEnemy.gameObject.SetActive(true);
        _enemies.Add(newEnemy);
        newEnemy.transform.position = position;
        return newEnemy;
    }
}
