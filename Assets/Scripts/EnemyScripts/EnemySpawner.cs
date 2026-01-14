using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyGroup
{
    public string groupName;
    public GameObject enemyPrefab;
    public int enemyCount;
}

[System.Serializable]
public class WaveData
{
    public string waveName;
    public List<EnemyGroup> EnemyGroups;
    public float spawnRate = 1f;
}

public class EnemySpawner : MonoBehaviour
{
    public List<WaveData> waves;
    public float spawnRadius = 10f;
    public float timeBetweenWaves = 3f;

    [HideInInspector]
    public int enemiesAlive = 0;

    private Transform player;
    private int currentWaveIndex = 0;

    void Start()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
        {
            player = p.transform;
        }

        StartCoroutine(StartWaveLoop());
    }

    IEnumerator StartWaveLoop()
    {
        while (currentWaveIndex < waves.Count)
        {
            WaveData currentWave = waves[currentWaveIndex];
            foreach (EnemyGroup group in currentWave.EnemyGroups)
            {
                for (int i = 0; i < group.enemyCount; i++)
                {
                    SpawnEnemy(group.enemyPrefab);

                    yield return new WaitForSeconds(currentWave.spawnRate);
                }
            }
            while (enemiesAlive > 0)
            {
                yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitForSeconds(timeBetweenWaves);
            currentWaveIndex++;
        }
    }

    void SpawnEnemy(GameObject prefab)
    {
        if (player == null) return;

        Vector2 randomPos = Random.insideUnitCircle.normalized;
        Vector3 spawnPos = player.position + (Vector3)(randomPos * spawnRadius);

        Instantiate(prefab, spawnPos, Quaternion.identity);
        enemiesAlive++;
    }
    public void OnEnemyKilled()
    {
        enemiesAlive--;
    }
}