using System;
using UnityEngine;

public class EnemyPlayerCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision Enter");
        bool isPlayerCollision = collision.gameObject.CompareTag("Player");
        if (isPlayerCollision)
        {
            if (collision.GetContact(0).normal.y <= -0.5)
            {
                gameObject.SetActive(false);
                collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 100);
            }
            else
            {
                collision.gameObject.GetComponent<MarioController>().HandlePlayerHit();
            }
        }
    }
}