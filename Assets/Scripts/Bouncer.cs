using System;
using UnityEngine;

public class Bouncer : MonoBehaviour
{
    public static EventHandler OnBounce;
    [SerializeField] private int bounceForce = 1000;

    private void OnCollisionEnter(Collision collision)
    {
        var isPlayerCollision = collision.gameObject.CompareTag("Player");
        if (!isPlayerCollision) return;
        if (collision.GetContact(0).normal.y <= -0.5)
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * bounceForce);
            OnBounce?.Invoke(this, EventArgs.Empty); 
        }
    }
}