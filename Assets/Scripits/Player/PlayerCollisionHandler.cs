using System.Collections;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    // private static WaitForSeconds _waitForSeconds1 = new(1f);
    [SerializeField] private Animator animator;
    [SerializeField] float collistionCooldown = 1f;
    [SerializeField] private float adjustChangeMoveSpeedAmount = -2f;
    float cooldownTimer = 0f;
    private const string hitStringTrigger = "Hit";
    LevelGenerator levelGenerator;

    private void Start()
    {
        levelGenerator = FindFirstObjectByType<LevelGenerator>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (cooldownTimer < collistionCooldown) return;

        levelGenerator.ChangeChunkMoveSpeed(adjustChangeMoveSpeedAmount);
        animator.SetTrigger(hitStringTrigger);
        cooldownTimer = 0f;
    }

    // void OnCollisionEnter(Collision collision)
    // {
    //     animator.SetTrigger(hitStringTrigger);
    //     StartCoroutine(ResetTriggerCoroutine());
    // }

    // private IEnumerator ResetTriggerCoroutine()
    // {
    //     yield return _waitForSeconds1;
    //     animator.ResetTrigger(hitStringTrigger);
    // }

}
