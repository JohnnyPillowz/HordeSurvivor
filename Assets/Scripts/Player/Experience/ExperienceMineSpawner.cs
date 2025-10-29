using System.Collections;
using UnityEngine;

public class ExperienceMineSpawner : MonoBehaviour
{
    [SerializeField] private ExperienceManager experienceManager;
    [SerializeField] private GameObject minePrefab;
    [SerializeField] private float spawnInterval = 3f;
    
    [Range(0, 100)] [SerializeField] private int minSpawnRange = 50;
    [Range(0, 100)] [SerializeField] private int maxSpawnRange = 75;

    private Player player;
    private void Start()
    {
        player = Player.Instance;
        StartCoroutine(SpawnMinesRoutine());
    }

    private IEnumerator SpawnMinesRoutine()
    {
        while (player)
        {
            SpawnMine();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnMine()
    {
        //choose random location outside camera
        float randomAngle = Random.Range(0, Mathf.PI * 2);
        float randomRange = Random.Range(minSpawnRange, maxSpawnRange);
        
        float xPos = Mathf.Cos(randomAngle)*randomRange;
        float zPos = Mathf.Sin(randomAngle)*randomRange;
        
        Vector3 calculatedPos = new Vector3(xPos, 0, zPos);
        Vector3 spawnPos = calculatedPos + player.transform.position;
        
        Mine mineSpawned = Instantiate(minePrefab, spawnPos, Quaternion.identity).GetComponent<Mine>();
        experienceManager.spawnedMines.Add(mineSpawned);
    }
}
