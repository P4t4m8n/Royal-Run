using UnityEngine;

public class Apple : Pickup
{
    [SerializeField] private float adjustChangeMoveSpeedAmount = 3f;
    private LevelGenerator levelGenerator;

    public void Init(LevelGenerator levelGenerator)
    {
        this.levelGenerator = levelGenerator;
    }

    protected override void OnPickup()
    {
        levelGenerator.ChangeChunkMoveSpeed(adjustChangeMoveSpeedAmount);
    }
}
