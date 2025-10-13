using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform playerTransform;
    [SerializeField]
    private Vector3 offsetPosition;

    private void LateUpdate()
    {
        transform.position = playerTransform.position + offsetPosition;
    }
}
