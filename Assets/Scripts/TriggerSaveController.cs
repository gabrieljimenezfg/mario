using UnityEngine;

public class TriggerSaveController : MonoBehaviour
{
    [SerializeField]
    private GameObject savePlatform;
    [SerializeField]
    private GameObject initialBridge;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            savePlatform.SetActive(true);
            initialBridge.SetActive(false);
        }
    }
}
