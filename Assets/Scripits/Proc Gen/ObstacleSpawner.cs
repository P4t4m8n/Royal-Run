using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] obstaclePrefabs;
    [SerializeField] private Transform obstacleParent;
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private float minSpawnInterval = 0.2f;
    [SerializeField] private float spawnWidth = 4f;
    void Start()
    {
        StartCoroutine(SpawnObstacleCoroutine());
    }

    public void DecreaseObstacleSpawnInterval(float amount)
    {
        spawnInterval -= amount;
        if (spawnInterval < minSpawnInterval) spawnInterval = minSpawnInterval;

    }

    private IEnumerator SpawnObstacleCoroutine()
    {
        while (true)
        {
            GameObject obstaclePrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
            Vector3 spawnPosition = new(Random.Range(-spawnWidth, spawnWidth), transform.position.y, transform.position.z);
            yield return new WaitForSeconds(spawnInterval);
            Instantiate(obstaclePrefab, spawnPosition, Random.rotation, obstacleParent);
        }
    }
}
