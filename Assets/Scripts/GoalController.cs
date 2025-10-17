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
        GameManager.instance.PickedUpCoin += GoalController_OnCoinPickedUp;
    }

    private void Start()
    {
        SetMissingCoinsText();
    }

    private void GoalController_OnCoinPickedUp(object sender, EventArgs e)
    {
        SetMissingCoinsText();
    }

    private void SetMissingCoinsText()
    {
        var missingCoins = CoinManager.Instance.coinsCountInLevel - GameManager.instance.totalCoins;
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

        var isCoinGoalReached = GameManager.instance.totalCoins >= CoinManager.Instance.coinsCountInLevel;
        if (!isCoinGoalReached) return;

        lm.WinLevel();
    }
}