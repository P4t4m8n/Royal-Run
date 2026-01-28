using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] private GameObject fencePrefab;
    [SerializeField] private GameObject applePrefab;
    [SerializeField] private GameObject coinPrefab;

    [SerializeField] private float[] lanes = { -2.5f, 0f, 2.5f };

    [SerializeField] private float appleSpawnChance = 0.3f;
    [SerializeField] private float coinSpawnChance = 0.5f;
    [SerializeField] private float coinSeperationlength = 2f;

    List<int> availableLanes = new() { 0, 1, 2 };

    private void Start()
    {
        SpawnFences();
        SpawnApple();
        SpawnCoin();
    }


    private void SpawnFences()
    {

        int fencesToSpawn = Random.Range(0, lanes.Length);

        for (int i = 0; i < fencesToSpawn; i++)
        {
            if (availableLanes.Count < 1) break;
            SpawnFance();
        }

    }

    private void SpawnFance()
    {
        SpawnObstacle(fencePrefab);
    }

    private void SpawnApple()
    {
        if (Random.value > appleSpawnChance) return;
        if (availableLanes.Count < 1) return;

        SpawnObstacle(applePrefab);
    }

    private void SpawnCoin()
    {
        if (Random.value > coinSpawnChance) return;
        if (availableLanes.Count < 1) return;

        int maxCoinsToSpawn = 5 + 1;
        int randomNumberOfCoins = Random.Range(1, maxCoinsToSpawn);
        int selectedLane = SelectLane();


        float topOfChunkZPosition = transform.position.z + (coinSeperationlength * 2f);

        for (int i = 0; i < randomNumberOfCoins; i++)
        {
            float spawnPositionZ = topOfChunkZPosition - (i * coinSeperationlength);
            SpawnObstacle(coinPrefab, spawnPositionZ, selectedLane);
        }
    }

    private int SelectLane()
    {
        int randomLaneIndex = Random.Range(0, availableLanes.Count);
        int selectedLane = availableLanes[randomLaneIndex];

        availableLanes.RemoveAt(randomLaneIndex);

        return selectedLane;
    }

    private void SpawnObstacle(GameObject obstaclePrefab, float? spawnPositionZ = null, int? specificLane = null)
    {
        int selectedLane = specificLane ?? SelectLane();

        float zPos = spawnPositionZ ?? transform.position.z;

        Vector3 spawnPosition = new(lanes[selectedLane], transform.position.y, zPos);
        Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity, this.transform);
    }


}
