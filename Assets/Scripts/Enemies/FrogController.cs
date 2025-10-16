using System;
using UnityEngine;

public class FrogController : MonoBehaviour, IEnemyWithVisionRange, IEnemyResettable
{
    private bool isPlayerInsideViewRange;
    private Rigidbody rb;

    [SerializeField] private int jumpForce;
    private Transform playerTransform;
    [SerializeField] private float jumpCooldown = 2;
    private float jumpCooldownTimer = 0;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void HandlePlayerSeen(Transform playerPosition)
    {
        isPlayerInsideViewRange = true;
        playerTransform = playerPosition;
    }

    public void HandlePlayerExitView()
    {
        isPlayerInsideViewRange = false;
    }

    private void RotateToFacePlayer()
    {
        var direction = playerTransform.position - transform.position;
        direction.y = 0;

        transform.forward = direction.normalized;
    }

    private void JumpTowardsPlayer()
    {
        if (jumpCooldownTimer > 0)
        {
            jumpCooldownTimer -= Time.deltaTime;
            return;
        }

        RotateToFacePlayer();
        var playerDirection = (playerTransform.position - transform.position);
        var multiplier = 1;
        if (playerDirection.x < 0)
        {
            multiplier = -1;
        }

        var jumpVectorXValue = multiplier * 1;
        var jumpVector = new Vector3(jumpVectorXValue, 1, 0);
        rb.AddForce(jumpVector * jumpForce);
        jumpCooldownTimer = jumpCooldown;
    }

    private void Update()
    {
        if (isPlayerInsideViewRange)
        {
            JumpTowardsPlayer();
        }
    }

    public void ResetEnemyState()
    {
        isPlayerInsideViewRange = false;
        jumpCooldownTimer =  jumpCooldown;
    }
}