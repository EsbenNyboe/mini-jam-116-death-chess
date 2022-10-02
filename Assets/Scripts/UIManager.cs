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
    }

    void Update()
    {
        float timeCalc = Time.fixedTime;

        // float scoreCalc = _score / timeCalc;
        float scoreCalc = _score;

        scoreText.text = "Score: " + (int)scoreCalc + " pts";
        timeText.text = "Time: " + (int)timeCalc + " sec";
    }

    public void AddToScore(int points)
    {
        _score += points;
    }
}