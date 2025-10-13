using System;
using UnityEngine;

public class EnemyViewRange : MonoBehaviour
{
    public event EventHandler SawPlayer;
    private void OnTriggerEnter(Collider other)
    {
        bool isPlayer = other.gameObject.CompareTag("Player");
        if (isPlayer)
        {
            SawPlayer?.Invoke(this, EventArgs.Empty);
        }
    }
}
