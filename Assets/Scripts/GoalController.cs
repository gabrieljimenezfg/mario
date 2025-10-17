using System;
using UnityEngine;
using UnityEngine.UI;

public class GoalController : MonoBehaviour
{
    private LevelManager lm;
    [SerializeField] private Text goalText;

    private void Awake()
    {
        lm = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    private void Start()
    {
        GameManager.Instance.OnTotalCoinsChange += GoalController_OnTotalCoinsChange;
        SetMissingCoinsText();
    }

    private void GoalController_OnTotalCoinsChange(object sender, EventArgs e)
    {
        SetMissingCoinsText();
    }

    private void SetMissingCoinsText()
    {
        Debug.Log("SetMissingCoinsText");
        var missingCoins = CoinManager.Instance.coinsCountInLevel - GameManager.Instance.totalCoins;
        if (missingCoins > 0)
        {
            goalText.text = "¡" + missingCoins + " más!";
        }
        else
        {
            goalText.text = "¡Palante!";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var isPlayer = other.gameObject.CompareTag("Player");
        if (!isPlayer) return;

        var isCoinGoalReached = GameManager.Instance.totalCoins >= CoinManager.Instance.coinsCountInLevel;
        if (!isCoinGoalReached) return;

        lm.WinLevel();
    }
}