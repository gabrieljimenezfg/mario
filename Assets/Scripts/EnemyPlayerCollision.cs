using System;
using UnityEngine;

public class EnemyPlayerCollision : MonoBehaviour
{
    [SerializeField] private int jumpForceOnStomp = 300;
    private void OnCollisionEnter(Collision collision)
    {
        var isPlayerCollision = collision.gameObject.CompareTag("Player");
        if (!isPlayerCollision) return;
        if (collision.GetContact(0).normal.y <= -0.5)
        {
            gameObject.SetActive(false);
            collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForceOnStomp);
        }
        else
        {
            collision.gameObject.GetComponent<MarioController>().HandlePlayerHit();
        }
    }
}