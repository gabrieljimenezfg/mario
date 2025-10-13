using System;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    private GameObject[] enemiesInLevel;

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
    }

    private void Start()
    {
        enemiesInLevel = GameObject.FindGameObjectsWithTag("Enemy");
    }

    public void RespawnAllEnemies()
    {
        foreach (var enemy in enemiesInLevel)
        {
            enemy.gameObject.SetActive(true);
            var enemyController = enemy.GetComponent<EnemyController>();
            enemyController.ResetOriginalPosition();
        }
    }
}
