using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private float timeBonusAmount = 5f;
    private GameManager gameManager;

    const string playerString = "Player";

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(playerString)) return;

        gameManager.IncreaseTime(timeBonusAmount);
    }
}
