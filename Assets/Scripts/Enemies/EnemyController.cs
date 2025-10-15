using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField]
    private float speed = 0;
    [SerializeField]
    private bool listo = false;

    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private bool following = false;

    private void Awake()
    {
        originalPosition = transform.position;
        originalRotation =  transform.rotation;
    }

    private void MoveLeft()
    {
        Vector3 movement = speed * Time.deltaTime * Vector3.left;
        transform.Translate(movement, Space.World);
    }

    void Update()
    {
        if (following) MoveLeft();
    }

    private void OnTriggerEnter(Collider other)
    {
        bool isPlayer = other.gameObject.CompareTag("Player");
        if (isPlayer)
        {
            following = true;
        }

        if (listo && other.gameObject.CompareTag("EnemyLimit"))
        {
            transform.eulerAngles += new Vector3(0, 180, 0);
            speed *= -1;
        }
    }

    public void ResetOriginalPosition()
    {
        following = false;
        gameObject.SetActive(true);
        transform.SetPositionAndRotation(originalPosition, originalRotation);
    }
}
