using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemySpawned
{
    [field: SerializeField] public EnemyNameEnum Name { get; private set; }
    [field: SerializeField] public GameObject EnemyPrefab { get; private set; }
    [field: SerializeField] public int MaxEnemyCount { get; private set; }
}

public class EnemySpawner : MonoBehaviour
{
    [field: SerializeField] public float Radius { get; private set; }
    [field: SerializeField] private float EnemySpawnInterval { get;  set; }
    [field: SerializeField] private List<EnemySpawned> EnemiesSpawned { get; set; }


    private void Start()
    {
        Initialize();
    }

    public void HandleEnemyDied(EnemyNameEnum name)
    {
        StartCoroutine(AutoSpawnEnemy(name));
    }

    private void Initialize()
    {
        foreach(EnemySpawned enemy in EnemiesSpawned)
        {
            for(int i = 0; i < enemy.MaxEnemyCount; i++)
            {
                Spawn(enemy);
            }
        }
    }

    private IEnumerator AutoSpawnEnemy(EnemyNameEnum name)
    {

        yield return new WaitForSeconds(EnemySpawnInterval);

        Spawn(GetEnemySpawned(name));

    }

    private EnemySpawned GetEnemySpawned(EnemyNameEnum name)
    {
        return EnemiesSpawned.Find(spawn => spawn.Name == name);
    }

    private void Spawn(EnemySpawned enemySpawned)
    {
        Vector3 spawnPosition = transform.position;
        Quaternion spawnRotation = transform.rotation;

        GameObject prefab = enemySpawned.EnemyPrefab;

        float theta = UnityEngine.Random.Range(0f, 360f);
        float phi = UnityEngine.Random.Range(0f, 360f);

        float x = Mathf.Sin(phi * Mathf.Deg2Rad) * Mathf.Cos(theta * Mathf.Deg2Rad) * Radius;
        float z = Mathf.Sin(phi * Mathf.Deg2Rad) * Mathf.Sin(theta * Mathf.Deg2Rad) * Radius;

        Vector3 minionSpawnPosition = spawnPosition + new Vector3(x, 0f, z);

        GameObject gameObject = Instantiate(prefab, minionSpawnPosition, spawnRotation);
        
        gameObject.name = gameObject.name.Replace("(Clone)", "").Trim();

        gameObject.GetComponent<Enemy>().SpawnerBy(this);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, Radius);
    }

}
