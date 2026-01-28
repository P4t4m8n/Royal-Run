using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{

    [SerializeField] private GameObject obstaclePrefab;

    [SerializeField] private float spawnInterval = 1f;
    void Start()
    {
        StartCoroutine(SpawnObstacleCoroutine());
    }

    private IEnumerator SpawnObstacleCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            Instantiate(obstaclePrefab, transform.position, Random.rotation);
        }
    }
}
