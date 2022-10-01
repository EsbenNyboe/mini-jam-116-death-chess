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
    private int _spawnIndex;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _spawnIndex = SerialLogicHelper.RollNewIndex(spawnPoints.Length, serialLogic, _spawnIndex);
            GameObject enemy = Instantiate(enemyTemplate, spawnPoints[_spawnIndex]);
            enemy.SetActive(true);
        }
    }
}