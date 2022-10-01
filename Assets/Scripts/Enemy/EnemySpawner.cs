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
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _spawnIndex = SerialLogicHelper.RollNewIndex(gameGridScript.width, serialLogic, _spawnIndex);
            Vector3 spawnPosition = gameGridScript.GetWorldPosFromGridPos(new Vector2Int(0, _spawnIndex));
            GameObject enemy = Instantiate(enemyTemplate, spawnPosition, Quaternion.identity);
            enemy.SetActive(true);
        }
    }
}