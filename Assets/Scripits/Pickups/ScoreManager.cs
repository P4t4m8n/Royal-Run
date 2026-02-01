using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    private int score = 0;
    [SerializeField] private TMPro.TMP_Text scoreboardText;

    public void HandleScore(int amount)
    {
        score += amount;
        scoreboardText.text = score.ToString();
    }

}
