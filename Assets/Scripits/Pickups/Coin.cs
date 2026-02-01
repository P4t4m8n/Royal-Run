
using UnityEngine;

public class Coin : Pickup
{

    [SerializeField] int coinValue = 1;
    private ScoreManager scoreManager;

    public void Init(ScoreManager scoreManager)
    {
        this.scoreManager = scoreManager;
    }
    protected override void OnPickup()
    {
        scoreManager.HandleScore(coinValue);
    }
}
