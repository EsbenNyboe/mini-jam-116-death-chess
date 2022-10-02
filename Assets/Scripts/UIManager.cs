using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    
    [FormerlySerializedAs("_scoreText")] [SerializeField] Text scoreText;
    [FormerlySerializedAs("_timeText")] [SerializeField] Text timeText;

    private float _score;
    private float _startTime;
    private float _timer;
    [SerializeField] private float timerFactor;
    [SerializeField] private int lives = 10;

    private bool _gameOver;

    private void Awake()
    {
        if (Instance)
        {
            if (Instance != this)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        scoreText.text = "Score: " + 0 + " pts";
        timeText.text = "Time: " + 0 + " sec";
        _timer = 0;
    }

    void Update()
    {
        _timer += Time.deltaTime;
        float timeCalc = _timer * timerFactor;

        float scoreCalc = _score / timeCalc;
        // float scoreCalc = _score;

        scoreText.text = "Score: " + (int)scoreCalc;// + " pts";
        // timeText.text = "Time: " + (int)_timer;// + " sec";
        timeText.text = "Lives: " + lives;// + " sec";
    }

    public void AddToScore(int points)
    {
        _score += points;
    }

    public void TakeDamage(int points)
    {
        lives -= points;
        if (lives < 1)
        {
            // PLAY SOUND: GAME OVER
            _gameOver = true;
        }
    }
}