using System;
using System.Collections;
using System.Collections.Generic;
using SfxSystem;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyTemplate;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private SerialLogicHelper.SerialLogic serialLogic;
    private int _spawnIndex = -1;

    [SerializeField] private GameGridScript gameGridScript;
    
    [Range(0.01f, 1f)]
    [SerializeField] private float spawnWaitTime = 0.5f;
    private float _spawnTimer;
    
    private void Update()
    {
        _spawnTimer += Time.deltaTime;
        if (_spawnTimer > spawnWaitTime)
        {
            _spawnTimer = 0;
            SpawnEnemy();
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        _spawnIndex = SerialLogicHelper.RollNewIndex(gameGridScript.width, serialLogic, _spawnIndex);
        Vector3 spawnPosition = gameGridScript.GetWorldPosFromGridPos(new Vector2Int(0, _spawnIndex));
        GameObject enemy = Instantiate(enemyTemplate, spawnPosition, Quaternion.identity);
        enemy.SetActive(true);
    }
}