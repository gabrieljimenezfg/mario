using System;
using UnityEngine;

public class BeeController : MonoBehaviour, IEnemyWithVisionRange
{
   [SerializeField]
   private Transform patrolStartPosition;
   [SerializeField]
   private Transform patrolEndPosition;

   private Vector3 currentPatrolTarget;
   
   private enum State {
      Idle,
      Targeting,
      Attacking,
   }

   [SerializeField] private int launchSpeed = 1;
   [SerializeField] private int patrolSpeed = 1;

   private State state;
   private Transform playerTransform;
   private Vector3 launchTargetPosition;

   [SerializeField]
   private float targetingDuration = 2f;
   private float targetingTimer = 0f;
   [SerializeField]
   private float maximumTargetingDuration = 10f;
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
      if (state != State.Idle) return;
      playerTransform = playerPosition;
      state = State.Targeting;
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
      transform.LookAt(playerTransform);
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
         targetingTimer = 0f;
         launchTargetPosition = playerTransform.position;
         playerTransform = null;
         state = State.Attacking;
      }
   }

   private void FinishAttacking()
   {
      state = State.Idle;
      launchTargetPosition = Vector3.zero;
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
}
