using System;
using System.Collections;
using System.Collections.Generic;
using SfxSystem;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

public class EnemyWaveSpawner : MonoBehaviour
{
    [SerializeField] private WaveSequence[] waveSequences;
    private int _spawnIndex = -1;

    private GameGridScript _gameGridScript;

    private int _currentSequenceIndex;
    private int _currentWaveIndex;
    private int _currentSpawnCount;
    private float _currentTime;
    private bool _isWaitingForClear;
    private int _enemiesToClear;

    private List<GameObject> _currentWaveEnemies;

    private WaveSequence.Wave _currentWave;

    private void Start()
    {
        _gameGridScript = GameGridScript.Instance;
        Enemy.OnEnemyKilled += OnEnemyKilled;
        _currentWaveEnemies = new List<GameObject>();
        _currentWave = waveSequences[0].Waves[0];
        MusicTriggerOnWaveStarted();
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

            if (_currentWaveEnemies.Count <= 0) // because who knows...
            {
                WaveClear();
            }
            return;
        }
        if (_currentTime >= _currentWave.spawnWaitTime)
        {
            _currentTime = 0;
            _currentSpawnCount++;
            SpawnEnemyNew();
            
            if (_currentSpawnCount >= _currentWave.count)
            {
                _currentSpawnCount = 0;
                if (_currentWave.waitForClear)
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
        _currentWaveIndex++;

        if (_currentWaveIndex >= waveSequences[_currentSequenceIndex].Waves.Length)
        {
            _currentWaveIndex = 0;
            _currentSequenceIndex++;
            if (_currentSequenceIndex >= waveSequences.Length)
            {
                gameObject.SetActive(false);
            }
        }
        
        _currentWave = waveSequences[_currentSequenceIndex].Waves[_currentWaveIndex];
        MusicTriggerOnWaveStarted();
    }

    private void OnEnemyKilled(GameObject killedEnemy)
    {
        if (_currentWave.waitForClear && _isWaitingForClear)
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
        MusicTriggerOnClear();
        NextWave();
    }

    private void SpawnEnemyNew()
    {
        _spawnIndex = SerialLogicHelper.RollNewIndex(_gameGridScript.height, 
            _currentWave.serialLogic, _spawnIndex);

        GridCellScript gridCellScript = _gameGridScript.GetGridCellScriptFromGridPos(new Vector2Int(0, _spawnIndex));
        if (gridCellScript.isOccupied)
        {
            _currentSpawnCount--;
            return;
        }
        Vector3 spawnPosition = _gameGridScript.GetWorldPosFromGridPos(new Vector2Int(0, _spawnIndex));

        if (_currentWave.enemy)
        {
            GameObject enemyTemplate = _currentWave.enemy;
            GameObject spawnedEnemy = Instantiate(enemyTemplate, spawnPosition, Quaternion.identity);
            if (_currentWave.waitForClear)
            {
                _currentWaveEnemies.Add(spawnedEnemy);
            }
        }
    }
    
    private void MusicTriggerOnWaveStarted()
    {
        switch (_currentWave.onStartMusicTrigger)
        {
            case WaveSequence.MusicTriggerOnStart.None:
                break;
            case WaveSequence.MusicTriggerOnStart.HighA:
                // insert FMOD stuff
                Debug.Log("Clear High");
                break;
        }
    }

    private void MusicTriggerOnClear()
    {
        switch (_currentWave.onClearMusicTrigger)
        {
            case WaveSequence.MusicTriggerOnClear.None:
                break;
            case WaveSequence.MusicTriggerOnClear.LowA:
                // insert FMOD stuff
                Debug.Log("Clear Low");
                break;
        }
    }
}