
using UnityEngine;

public interface IEnemyWithVisionRange 
{
    public void HandlePlayerSeen(Transform playerPosition);

    public void HandlePlayerExitView();
}