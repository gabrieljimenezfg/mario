using UnityEngine;

public class EnemyRespawnRestore : MonoBehaviour
{
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    
    private void Awake()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    public void ResetOriginalPosition()
    {
        transform.SetPositionAndRotation(originalPosition, originalRotation);
    }
}
