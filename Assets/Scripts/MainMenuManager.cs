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
        GameManager.instance.healthPoints = 3;
        GameManager.instance.totalCoins = 0;
        SceneManager.LoadScene(1);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
