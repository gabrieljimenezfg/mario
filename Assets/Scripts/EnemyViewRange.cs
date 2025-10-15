using System;
using UnityEngine;

public class EnemyViewRange : MonoBehaviour
{
    private IEnemyWithVisionRange enemy;
    private void Awake()
    {
        enemy = gameObject.GetComponentInParent<IEnemyWithVisionRange>();
    }

    private void OnTriggerEnter(Collider other)
    {
        bool isPlayer = other.gameObject.CompareTag("Player");
        if (isPlayer)
        {
            enemy.HandlePlayerSeen(other.transform);
        }
    }
}
