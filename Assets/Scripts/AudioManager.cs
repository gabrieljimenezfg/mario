using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    private AudioSource audioSource;
    [SerializeField] private AudioClip coinPickupSound;
    [SerializeField] private AudioClip enemyStompSound;
    [SerializeField] private AudioClip boingSound;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip smashSound;

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
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = 0.2f;
    }

    private void PlaySound(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }

    public void PlayCoinPickup()
    {
        PlaySound(coinPickupSound);
    }
    
    public void PlayEnemyStomp()
    {
        PlaySound(enemyStompSound);
    }
    
    public void PlayBounce()
    {
        PlaySound(boingSound);
    }
    
    public void PlayJump()
    {
        PlaySound(jumpSound);
    }
    
    public void PlaySmash()
    {
        PlaySound(smashSound);
    }
}