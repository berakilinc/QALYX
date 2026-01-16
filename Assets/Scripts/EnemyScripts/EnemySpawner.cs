using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    
    public UpgradeManager upgradeManager;
    private bool isWaitingForUpgrade = false;

    public TMP_Text waveCounterText;
    

    void Start()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
        {
            player = p.transform;
        }

        UpdateWaveUI();
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
            if (upgradeManager != null)
            {
                isWaitingForUpgrade = true;

                upgradeManager.OpenUpgradePanel();

                while (isWaitingForUpgrade)
                {
                    yield return null; 
                }
            }
            yield return new WaitForSeconds(timeBetweenWaves);
            currentWaveIndex++;
            UpdateWaveUI();
        }
    }

    void UpdateWaveUI()
    {
        if (waveCounterText != null)
        {
            waveCounterText.text = "" + (currentWaveIndex + 1);
        }
    }

    public void ResumeSpawner()
    {
        isWaitingForUpgrade = false;
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