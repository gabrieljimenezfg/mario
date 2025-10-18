using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    private AudioSource musicSource;
    
    private void SetInstance()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Awake()
    {
        SetInstance();
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.volume = 0.03f;
    }
    
    public void PlayMusic(AudioClip musicSong)
    {
        Debug.Log("Playing music");
        musicSource.clip = musicSong;
        musicSource.Play();
    }
}
