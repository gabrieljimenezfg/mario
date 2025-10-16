using UnityEngine;

public class FrogController : MonoBehaviour
{
    private bool isPlayerInsideViewRange;
    
    public void HandlePlayerSeen(Transform playerPosition)
    {
        isPlayerInsideViewRange = true;
    }

    public void HandlePlayerExitView()
    {
        isPlayerInsideViewRange = false;
    }
}
