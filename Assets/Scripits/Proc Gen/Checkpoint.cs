using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private float timeBonusAmount = 5f;
    [SerializeField] private float obstacleSpawnIntervalDecrease = 0.1f;
    private GameManager gameManager;
    private ObstacleSpawner obstacleSpawner;

    const string playerString = "Player";

    void Start()
    {
        obstacleSpawner = FindFirstObjectByType<ObstacleSpawner>();
        gameManager = FindFirstObjectByType<GameManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(playerString)) return;

        gameManager.IncreaseTime(timeBonusAmount);
        obstacleSpawner.DecreaseObstacleSpawnInterval(obstacleSpawnIntervalDecrease);
    }
}
