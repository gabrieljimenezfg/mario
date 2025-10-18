using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip coinPickupSound;
    [SerializeField] private AudioClip enemyStompSound;
    [SerializeField] private AudioClip boingSound;
    [SerializeField] private AudioClip jumpSound;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = 0.2f;
    }

    private void Start()
    {
        GameManager.Instance.OnCoinPickUp += AudioManager_OnPickedUpCoin;
        EnemyPlayerCollision.OnEnemyStomped += AudioManager_OnEnemyStomp;
        Bouncer.OnBounce += AudioManager_OnBounce;
    }

    private void PlaySound(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }

    private void AudioManager_OnPickedUpCoin(object sender, EventArgs e)
    {
        PlaySound(coinPickupSound);
    }
    
    private void AudioManager_OnEnemyStomp(object sender, EventArgs e)
    {
        PlaySound(enemyStompSound);
    }
    
    private void AudioManager_OnBounce(object sender, EventArgs e)
    {
        PlaySound(boingSound);
    }
    
    private void AudioManager_OnJump(object sender, EventArgs e)
    {
        PlaySound(jumpSound);
    }
}