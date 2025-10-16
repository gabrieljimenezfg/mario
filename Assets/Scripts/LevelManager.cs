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
    private Text coinsText, healthText;
    [SerializeField]
    private GameObject gameOverPanel, pausePanel;

    [SerializeField] private int levelCoinGoal;
    
    public void ToggleGamePause()
    {
        bool isPauseActive = pausePanel.activeInHierarchy;
        Time.timeScale = isPauseActive ? 1 : 0;
        pausePanel.SetActive(!isPauseActive);
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
    }

    public void UpdateHealth()
    {

        healthText.text = "x" + GameManager.instance.healthPoints;
    }

    public void UpdateCoins()
    {
        coinsText.text = "x" + GameManager.instance.totalCoins;
    } 

    public void Restart()
    {
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        GameManager.instance.healthPoints = 3;
        GameManager.instance.totalCoins = 0;
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
    }

    private void Start()
    {
        UpdateHealth();
        UpdateCoins();
        // HandlePlayerRespawn();
        AudioManager.instance.PlayMusic(musicSong);
    }
}
