using System;
using UnityEngine;

public class GoalController : MonoBehaviour
{
    private LevelManager lm;

    private void Awake()
    {
        lm = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var isPlayer = other.gameObject.CompareTag("Player");
        if (!isPlayer) return;
        
        var isCoinGoalReached = GameManager.instance.totalCoins >= CoinManager.Instance.coinsCountInLevel;
        if (!isCoinGoalReached) return;
        
        lm.WinLevel();
    }
}