using System;
using UnityEngine;

public class EnemyController : MonoBehaviour, IEnemyResettable
{
    [SerializeField] private float speed = 0;

    [SerializeField] private float idleScale = 0.5f;
    [SerializeField] private float speedAggroMultiplier = 1.5f;
    [SerializeField] private float scaleAggroMultiplier = 1.3f;
    private bool following = false;

    private void Awake()
    {
        transform.localScale = new Vector3(idleScale, idleScale, idleScale);
    }

    private void Move()
    {
        var speedMultiplier = following ? speedAggroMultiplier : 1;
        var movement = (speed * speedMultiplier) * Time.deltaTime * Vector3.left;
        transform.Translate(movement, Space.World);
    }

    void Update()
    {
        Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        bool isPlayer = other.gameObject.CompareTag("Player");
        if (isPlayer)
        {
            transform.localScale = new Vector3(scaleAggroMultiplier, scaleAggroMultiplier, scaleAggroMultiplier);
            following = true;
        }

        if (other.gameObject.CompareTag("EnemyLimit"))
        {
            transform.eulerAngles += new Vector3(0, 180, 0);
            speed *= -1;
        }
    }

    public void ResetEnemyState()
    {
        following = false;
        transform.localScale = new Vector3(idleScale, idleScale, idleScale);
        speed = speed < 0 ? speed * -1 : speed;
    }
}