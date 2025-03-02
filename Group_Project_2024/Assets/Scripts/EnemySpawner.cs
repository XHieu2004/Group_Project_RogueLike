﻿using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject enemySpawnPoint; 
    public float spawnInterval = 5f;
    public int maxEnemies = 100;
    private int currentEnemyCount = 0;

    void Start()
    {
        //StartCoroutine(SpawnEnemyCoroutine());
    }

    public IEnumerator SpawnEnemyCoroutine()
    {
        while (currentEnemyCount < maxEnemies)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        if (enemySpawnPoint == null)
        {
            Debug.LogError("Enemy Spawn Point is not assigned!");
            return;
        }

        Vector2 spawnPosition = enemySpawnPoint.transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(spawnPosition, out hit, 1.0f, NavMesh.AllAreas))
        {
            Instantiate(enemyPrefab, hit.position, Quaternion.identity);
            currentEnemyCount++;
        }
        else
        {
            Debug.LogWarning("Spawn Error, object not on navmesh surface");
        }
    }

    public void ResetEnemyCount()
    {
        currentEnemyCount = 0;
    }

    public IEnumerator SpawnBossEnemyCoroutine()
    {
        while (currentEnemyCount < maxEnemies)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }
        ResetEnemyCount();
    }

    public void DecreaseEnemyCount()
    {
        currentEnemyCount--;
    }
}
