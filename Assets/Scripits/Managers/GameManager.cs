using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] TMP_Text timeText;
    [SerializeField] GameObject gameOverText;
    [SerializeField] float startTime = 4f;
    float timeLeft;
    private bool isGameOver = false;
    public bool IsGameOver => isGameOver;

    private void Start()
    {
        timeLeft = startTime;
    }

    private void Update()
    {
        if (isGameOver) return;

        UpdateTime();

        if (timeLeft <= 0) GameOver();
    }

    private void UpdateTime()
    {
        timeLeft -= Time.deltaTime;
        timeText.text = timeLeft.ToString("F2");
    }

    private void GameOver()
    {
        isGameOver = true;
        playerController.enabled = false;
        gameOverText.SetActive(true);
        Time.timeScale = .1f;
    }
}
