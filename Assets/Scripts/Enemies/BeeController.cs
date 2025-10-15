using System;
using UnityEngine;

public class BeeController : MonoBehaviour, IEnemyWithVisionRange
{
   private EnemyViewRange enemyViewRange;

   private void Awake()
   {
      enemyViewRange = gameObject.GetComponentInChildren<EnemyViewRange>();
   }

   public void HandlePlayerSeen()
   {
      Debug.Log("Player Seen Interface");
   }
}
