using System;
using System.Collections;
using System.Collections.Generic;
using SfxSystem;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyWaveSpawner : MonoBehaviour
{
    [SerializeField] private WaveSequence[] waveSequences;
    private int _spawnIndex = -1;

    private GameGridScript _gameGridScript;

    private int _currentSequence;
    private int _currentWave;
    private int _currentCount;
    private float _currentTime;
    private bool _isWaitingForClear;
    private int _enemiesToClear;

    private List<GameObject> _currentWaveEnemies;

    private void Start()
    {
        _gameGridScript = GameGridScript.Instance;
        Enemy.OnEnemyKilled += OnEnemyKilled;
        _currentWaveEnemies = new List<GameObject>();
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;
        if (_isWaitingForClear)
        {
            if (_currentTime > 0.1f)
            {
                _currentTime = 0;
                for (var i = 0; i < _currentWaveEnemies.Count; i++)
                {
                    if (_currentWaveEnemies[i] == null)
                    {
                        _currentWaveEnemies.RemoveAt(i);
                    }
                }
            }

            if (_currentWaveEnemies.Count >= 1)
            {
                return;
            }
        }
        if (_currentTime >= waveSequences[_currentSequence].Waves[_currentWave].spawnWaitTime)
        {
            _currentTime = 0;
            _currentCount++;
            SpawnEnemyNew();
            
            if (_currentCount >= waveSequences[_currentSequence].Waves[_currentWave].count)
            {
                _currentCount = 0;
                if (waveSequences[_currentSequence].Waves[_currentWave].waitForClear)
                {
                    _isWaitingForClear = true;
                    return;
                }

                NextWave();
            }
        }
    }

    private void NextWave()
    {
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

    private void OnEnemyKilled(GameObject killedEnemy)
    {
        if (waveSequences[_currentSequence].Waves[_currentWave].waitForClear && _isWaitingForClear)
        {
            for (var i = 0; i < _currentWaveEnemies.Count; i++)
            {
                if (_currentWaveEnemies[i] == killedEnemy)
                {
                    _currentWaveEnemies.Remove(killedEnemy);
                    if (_currentWaveEnemies.Count <= 0)
                    {
                        WaveClear();
                    }
                }
            }
        }
    }

    private void WaveClear()
    {
        _isWaitingForClear = false;
        NextWave();
    }

    private void SpawnEnemyNew()
    {
        _spawnIndex = SerialLogicHelper.RollNewIndex(_gameGridScript.height, 
            waveSequences[_currentSequence].Waves[_currentWave].serialLogic, _spawnIndex);

        GridCellScript gridCellScript = _gameGridScript.GetGridCellScriptFromGridPos(new Vector2Int(0, _spawnIndex));
        if (gridCellScript.isOccupied)
        {
            _currentCount--;
            return;
        }
        Vector3 spawnPosition = _gameGridScript.GetWorldPosFromGridPos(new Vector2Int(0, _spawnIndex));

        if (waveSequences[_currentSequence].Waves[_currentWave].enemy)
        {
            GameObject enemyTemplate = waveSequences[_currentSequence].Waves[_currentWave].enemy;
            GameObject spawnedEnemy = Instantiate(enemyTemplate, spawnPosition, Quaternion.identity);
            if (waveSequences[_currentSequence].Waves[_currentWave].waitForClear)
            {
                _currentWaveEnemies.Add(spawnedEnemy);
            }
        }
    }
}