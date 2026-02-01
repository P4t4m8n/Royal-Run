using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

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
    private LevelGenerator levelGenerator;
    private ScoreManager scoreManager;

    private void Start()
    {
        SpawnFences();
        SpawnApple();
        SpawnCoins();
    }

    public void Init(LevelGenerator levelGenerator, ScoreManager scoreManager)
    {
        this.levelGenerator = levelGenerator;
        this.scoreManager = scoreManager;
    }
    private void SpawnFences()
    {
        int fencesToSpawn = Random.Range(0, lanes.Length);

        for (int i = 0; i < fencesToSpawn; i++)
        {
            if (availableLanes.Count < 1) break;
            SpawnFence();
        }
    }

    private void SpawnFence()
    {
        SpawnObstacle(fencePrefab);
    }

    private void SpawnApple()
    {
        if (Random.value > appleSpawnChance) return;
        if (availableLanes.Count < 1) return;

        Apple newApple = SpawnObstacle(applePrefab).GetComponent<Apple>();
        newApple.Init(levelGenerator);
    }

    private void SpawnCoins()
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
            Coin newCoin = SpawnObstacle(coinPrefab, spawnPositionZ, selectedLane).GetComponent<Coin>();
            newCoin.Init(scoreManager);
        }
    }

    private int SelectLane()
    {
        int randomLaneIndex = Random.Range(0, availableLanes.Count);
        int selectedLane = availableLanes[randomLaneIndex];

        availableLanes.RemoveAt(randomLaneIndex);

        return selectedLane;
    }

    private GameObject SpawnObstacle(GameObject obstaclePrefab, float? spawnPositionZ = null, int? specificLane = null)
    {
        int selectedLane = specificLane ?? SelectLane();

        float zPos = spawnPositionZ ?? transform.position.z;

        Vector3 spawnPosition = new(lanes[selectedLane], transform.position.y, zPos);
        return Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity, this.transform);
    }


}
