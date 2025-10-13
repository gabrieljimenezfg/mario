using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private AudioSource musicSource;
    private AudioSource sfxSource;

    private void SetInstance()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Awake()
    {
        SetInstance();
        musicSource = gameObject.AddComponent<AudioSource>();
        sfxSource = gameObject.AddComponent<AudioSource>();
    }

    public void PlayMusic(AudioClip musicSong)
    {
        musicSource.clip = musicSong;
        musicSource.Play();
    }

    public void PlaySFXSound(AudioClip sfx)
    {
        sfxSource.clip = sfx;
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}
