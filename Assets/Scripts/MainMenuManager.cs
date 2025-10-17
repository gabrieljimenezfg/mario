using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private void Awake()
    {
        Time.timeScale = 1;
    }

    public void PlayButton()
    {
        GameManager.Instance.healthPoints = 3;
        GameManager.Instance.totalCoins = 0;
        SceneManager.LoadScene(1);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
