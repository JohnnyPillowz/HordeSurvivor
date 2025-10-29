using System.Collections;
using UnityEngine;

public class ExperienceMineSpawner : MonoBehaviour
{
    [SerializeField] private ExperienceManager experienceManager;
    
    [Header("Spawner Settings")]
    [SerializeField] private float spawnInterval = 3f;
    [Range(0, 100)] [SerializeField] private int minSpawnRange = 50;
    [Range(0, 100)] [SerializeField] private int maxSpawnRange = 75;
    
    [Header("Mine Settings")]
    [SerializeField] private GameObject minePrefab;
    [SerializeField] private float mineHealth = 10f;
    [SerializeField] private int mineExperience = 7;

    private Player player;
    private void Start()
    {
        player = Player.Instance;
        SpawnMine(5, minSpawnRange);
        StartCoroutine(SpawnMinesRoutine());
    }

    private IEnumerator SpawnMinesRoutine()
    {
        while (player)
        {
            SpawnMine(minSpawnRange, maxSpawnRange);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnMine(int minRange, int maxRange)
    {
        //choose random location outside camera
        float randomAngle = Random.Range(0, Mathf.PI * 2);
        float randomRange = Random.Range(minRange, maxRange);
        
        float xPos = Mathf.Cos(randomAngle)*randomRange;
        float zPos = Mathf.Sin(randomAngle)*randomRange;
        
        Vector3 calculatedPos = new Vector3(xPos, 0, zPos);
        Vector3 spawnPos = calculatedPos + player.transform.position;
        
        Mine mineSpawned = Instantiate(minePrefab, spawnPos, Quaternion.identity).GetComponent<Mine>();
        mineSpawned.Init(mineExperience, mineHealth);
        experienceManager.spawnedMines.Add(mineSpawned);
    }
}
