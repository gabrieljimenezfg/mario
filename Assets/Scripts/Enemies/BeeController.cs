using System;
using UnityEngine;

public class BeeController : MonoBehaviour
{
   private EnemyViewRange enemyViewRange;

   private void Awake()
   {
      enemyViewRange = gameObject.GetComponentInChildren<EnemyViewRange>();
   }

   private void Start()
   {
      enemyViewRange.SawPlayer += BeeController_HandlePlayerSeen;
   }

   private void BeeController_HandlePlayerSeen(object sender, System.EventArgs e)
   {
      Debug.Log("Player Seen");
   }
}
