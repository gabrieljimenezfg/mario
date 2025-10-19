using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public event EventHandler OnTotalCoinsChange;
    
    public static GameManager Instance;
    public int healthPoints = 3;
    public int totalCoins = 0;

    public void IncreaseOneCoin()
    {
        totalCoins++;
        AudioManager.Instance.PlayCoinPickup();
    }

    public void RefreshCoinGoalUI()
    {
        OnTotalCoinsChange?.Invoke(this, EventArgs.Empty);
    }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }
}
