using System;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;
    private GameObject[] coinsInLevel;
    public int coinsCountInLevel;

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

    private void Start()
    {
        coinsInLevel = GameObject.FindGameObjectsWithTag("Coin");
        coinsCountInLevel = coinsInLevel.Length;
    }

    public void RespawnAllCoins()
    {
        foreach (var coin in coinsInLevel)
        {
            coin.gameObject.SetActive(true);
        }
    }
}