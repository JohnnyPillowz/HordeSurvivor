
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyPrefabs = new List<GameObject>();
    [SerializeField] private float spawnInterval = 2f;

    [Range(0, 100)]
    [SerializeField] private int minSpawnRange = 50;
    
    [Range(0, 100)]
    [SerializeField] private int maxSpawnRange = 75;

    private EnemyManager enemyManager;
    private Player player;
    
    private void Start()
    {
        player = Player.Instance;
        enemyManager = EnemyManager.Instance;
        StartCoroutine(SpawnEnemiesRoutine());
    }

    private IEnumerator SpawnEnemiesRoutine()
    {
        while (player)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        //choose random location outside camera
        float randomAngle = Random.Range(0, Mathf.PI * 2);
        float randomRange = Random.Range(minSpawnRange, maxSpawnRange);
        
        float xPos = Mathf.Cos(randomAngle)*randomRange;
        float zPos = Mathf.Sin(randomAngle)*randomRange;
        
        Vector3 calculatedPos = new Vector3(xPos, 0, zPos);
        Vector3 spawnPos = calculatedPos + player.transform.position;

        int enemyIndex = Random.Range(0, enemyPrefabs.Count);
        GameObject enemySpawned = Instantiate(enemyPrefabs[enemyIndex], spawnPos, Quaternion.identity);
        enemyManager.enemies.Add(enemySpawned);
    }
}
