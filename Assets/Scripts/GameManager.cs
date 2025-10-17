using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public event EventHandler PickedUpCoin;
    
    public static GameManager instance;
    public int healthPoints = 3;
    public int totalCoins = 0;

    public void IncreaseOneCoin()
    {
        totalCoins++;
        PickedUpCoin?.Invoke(this, EventArgs.Empty);
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }
}
