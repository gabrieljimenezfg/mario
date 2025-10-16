using System;
using UnityEngine;

public class BeeController : MonoBehaviour, IEnemyWithVisionRange, IEnemyResettable
{
    [SerializeField] private Transform patrolStartPosition;
    [SerializeField] private Transform patrolEndPosition;

    private Vector3 currentPatrolTarget;
    private bool isPlayerInsideViewRange = false;

    private enum State
    {
        Idle,
        Targeting,
        Attacking,
    }

    [SerializeField] private int launchSpeed = 1;
    [SerializeField] private int patrolSpeed = 1;

    private State state;
    private Transform playerTransform;
    private Vector3 launchTargetPosition;
    [SerializeField] private float targetingDuration = 2f;
    private float targetingTimer = 0f;
    [SerializeField] private float maximumTargetingDuration = 10f;
    private float elapsedTimeLaunchingTimer = 0f;

    private void Awake()
    {
        state = State.Idle;
        transform.position = patrolStartPosition.position;
        currentPatrolTarget = patrolEndPosition.position;
    }

    private void Start()
    {
        patrolStartPosition.transform.parent = null;
        patrolEndPosition.transform.parent = null;
    }

    public void HandlePlayerSeen(Transform playerPosition)
    {
        isPlayerInsideViewRange = true;
        playerTransform = playerPosition;
        if (state != State.Idle) return;
        state = State.Targeting;
    }

    public void HandlePlayerExitView()
    {
        isPlayerInsideViewRange = false;
    }

    private void TogglePatrolPoint()
    {
        if (currentPatrolTarget == patrolStartPosition.position)
        {
            currentPatrolTarget = patrolEndPosition.position;
        }
        else
        {
            currentPatrolTarget = patrolStartPosition.position;
        }
    }

    private void Patrol()
    {
        var distanceFromNextPatrolPoint = Vector3.Distance(currentPatrolTarget, transform.position);
        if (distanceFromNextPatrolPoint > 1)
        {
            transform.LookAt(currentPatrolTarget);
            var direction = currentPatrolTarget - transform.position;
            var movement = patrolSpeed * Time.deltaTime * direction.normalized;
            transform.position += movement;
        }
        else
        {
            TogglePatrolPoint();
        }
    }

    private void RotateToFacePlayer()
    {
        var direction = playerTransform.position - transform.position;
        // prevenimos que la abeja pueda mirar para arriba para siempre poder aplastarla
        direction.y = Mathf.Clamp(direction.y, -1, 0);
        
        transform.forward = direction.normalized;
    }

    private void TargetPlayer()
    {
        targetingTimer += Time.deltaTime;
        if (targetingTimer < targetingDuration)
        {
            RotateToFacePlayer();
        }
        else
        {
            state = State.Attacking;
            targetingTimer = 0f;
            launchTargetPosition = playerTransform.position;
        }
    }

    private void FinishAttacking()
    {
        elapsedTimeLaunchingTimer = 0f;
        if (isPlayerInsideViewRange)
        {
            state = State.Targeting;
        }
        else
        {
            playerTransform = null;
            state = State.Idle;
        }
    }

    private void LaunchTowardsPlayer()
    {
        elapsedTimeLaunchingTimer += Time.deltaTime;
        if (elapsedTimeLaunchingTimer >= maximumTargetingDuration)
        {
            FinishAttacking();
        }
        else
        {
            var distanceFromLaunchTarget = Vector3.Distance(launchTargetPosition, transform.position);
            if (distanceFromLaunchTarget > 1)
            {
                var direction = launchTargetPosition - transform.position;
                var movement = launchSpeed * Time.deltaTime * direction.normalized;
                transform.position += movement;
            }
            else
            {
                FinishAttacking();
            }
        }
    }

    private void Update()
    {
        switch (state)
        {
            case State.Idle:
                Patrol();
                break;

            case State.Targeting:
                TargetPlayer();
                break;

            case State.Attacking:
                LaunchTowardsPlayer();
                break;

            default:
                break;
        }
    }

    public void ResetEnemyState()
    {
        state = State.Idle;
        isPlayerInsideViewRange = false;
        currentPatrolTarget = patrolStartPosition.position;
        playerTransform = null;
        elapsedTimeLaunchingTimer = 0f;
        targetingTimer = 0f;
    }
}