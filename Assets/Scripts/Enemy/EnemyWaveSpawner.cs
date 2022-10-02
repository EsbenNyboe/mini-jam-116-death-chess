using System;
using System.Collections;
using System.Collections.Generic;
using SfxSystem;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyWaveSpawner : MonoBehaviour
{
    [SerializeField] private WaveSequence[] waveSequences;
    private int _spawnIndex;

    [SerializeField] private GameGridScript gameGridScript;

    private int _currentSequence;
    private int _currentWave;
    private int _currentCount;
    private float _currentTime;

    private void Update()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime >= waveSequences[_currentSequence].Waves[_currentWave].spawnWaitTime)
        {
            _currentTime = 0;
            _currentCount++;
            SpawnEnemyNew();
            
            if (_currentCount >= waveSequences[_currentSequence].Waves[_currentWave].count)
            {
                _currentCount = 0;
                _currentWave++;
                if (_currentWave >= waveSequences[_currentSequence].Waves.Length)
                {
                    _currentWave = 0;
                    _currentSequence++;
                    if (_currentSequence >= waveSequences.Length)
                    {
                        gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    private void SpawnEnemyNew()
    {
        _spawnIndex = SerialLogicHelper.RollNewIndex(gameGridScript.height, 
            waveSequences[_currentSequence].Waves[_currentWave].serialLogic, _spawnIndex);

        GridCellScript gridCellScript = gameGridScript.GetGridCellScriptFromGridPos(new Vector2Int(0, _spawnIndex));
        if (gridCellScript.isOccupied)
        {
            _currentCount--;
            return;
        }
        Vector3 spawnPosition = gameGridScript.GetWorldPosFromGridPos(new Vector2Int(0, _spawnIndex));

        if (waveSequences[_currentSequence].Waves[_currentWave].enemy)
        {
            GameObject enemyTemplate = waveSequences[_currentSequence].Waves[_currentWave].enemy;
            Instantiate(enemyTemplate, spawnPosition, Quaternion.identity);
        }
    }
}