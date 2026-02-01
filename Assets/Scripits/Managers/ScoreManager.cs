using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    private int score = 0;
    [SerializeField] private TMPro.TMP_Text scoreboardText;
    [SerializeField] private GameManager gameManager;

    public void HandleScore(int amount)
    {
        if (gameManager == null || gameManager.IsGameOver) return;

        score += amount;
        scoreboardText.text = score.ToString();
    }

}
