using UnityEngine;
using UnityEngine.AI; 
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 5f;
    public int maxEnemies = 100;
    public Vector2 spawnAreaMin;
    public Vector2 spawnAreaMax;
    private int currentEnemyCount = 0;

    void Start()
    {
        StartCoroutine(SpawnEnemyCoroutine());
    }

    private IEnumerator SpawnEnemyCoroutine()
    {
        while (true)
        {
            if (currentEnemyCount < maxEnemies)
            {
                SpawnEnemy();
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        Vector3 randomPosition = new Vector3(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            1f, 
            Random.Range(spawnAreaMin.y, spawnAreaMax.y)
        );

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPosition, out hit, 1.0f, NavMesh.AllAreas))
        {
            
            Instantiate(enemyPrefab, hit.position, Quaternion.identity);
            currentEnemyCount++;
        }
        else
        {
            Debug.LogWarning("Spawn Error, object not on navmesh surface");
        }
    }

    public void DecreaseEnemyCount()
    {
        currentEnemyCount--;
    }
}
