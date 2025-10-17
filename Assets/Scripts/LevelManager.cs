using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Transform spawnPoint;
    [SerializeField]
    private AudioClip musicSong;
    private Transform playerTransform;
    
    // UI
    [SerializeField]
    private Text coinsText, healthText, goalText;
    [SerializeField]
    private GameObject gameOverPanel, pausePanel, winPanel;
    
    public void ToggleGamePause()
    {
        var isPauseActive = pausePanel.activeInHierarchy;
        Time.timeScale = isPauseActive ? 1 : 0;
        pausePanel.SetActive(!isPauseActive);
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
    }

    public void LoadNextLevel()
    {
        var activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        var sceneCount = SceneManager.sceneCount;
        if (sceneCount > activeSceneIndex)
        {
            SceneManager.LoadScene(activeSceneIndex + 1);
        }
        else
        {
            GoToMainMenu();
        }
    }

    public void WinLevel()
    {
        Time.timeScale = 0;
        winPanel.SetActive(true);
    }
    
    public void UpdateHealth()
    {

        healthText.text = "x" + GameManager.Instance.healthPoints;
    }

    public void UpdateCoins()
    {
        coinsText.text = GameManager.Instance.totalCoins + " / " + CoinManager.Instance.coinsCountInLevel;
        GameManager.Instance.RefreshCoinGoalUI();
    } 

    public void Restart()
    {
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        GameManager.Instance.healthPoints = 3;
        GameManager.Instance.totalCoins = 0;
        SceneManager.LoadScene(activeSceneIndex);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void Awake()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
        Time.timeScale = 1.0f;
    }

    public void HandlePlayerRespawn()
    {
        playerTransform.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);
        EnemyManager.Instance.RespawnAllEnemies();
        GameManager.Instance.totalCoins = 0;
        UpdateCoins(); 
        CoinManager.Instance.RespawnAllCoins();
    }

    private void Start()
    {
        UpdateHealth();
        UpdateCoins();
        HandlePlayerRespawn();
        AudioManager.instance.PlayMusic(musicSong);
    }
}
