using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyTemplate;
    [SerializeField] private Transform[] spawnPoints;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject enemy = Instantiate(enemyTemplate, spawnPoints[Random.Range(0, spawnPoints.Length)]);
            enemy.SetActive(true);
        }
    }
}