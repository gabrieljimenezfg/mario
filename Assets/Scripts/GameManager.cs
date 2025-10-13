using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int healthPoints = 3;
    public int totalCoins = 0;

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
