using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
[SerializeField] Text _scoreText;
[SerializeField] Text _timeText;

float pawnPts = 0f;
float runnerPts = 0f;
float towerPts = 0f;
float knightPts = 0f;
float queenPts = 0f;
float kingPts = 0f;

    void Start()
    {
        _scoreText.text = "Score: " + 0 + " pts";
        _timeText.text = "Time: " + 0 + " sec";
    }

    void Update()
    {        
        float timeCalc = Time.fixedTime;

        float scoreCalc = 
        (pawnPts + 
        runnerPts + 
        towerPts +
        knightPts +
        queenPts +
        kingPts) /
        timeCalc;
        
        if (Input.GetKeyDown(KeyCode.P))
        {
            pawnPts += 1f;
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            runnerPts += 5f;
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            towerPts += 10f;
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            knightPts += 20f;
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            queenPts += 50f;
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            kingPts += 100f;
        }

        _scoreText.text = "Score: " + (int) scoreCalc + " pts";
        _timeText.text = "Time: " + (int) timeCalc + " sec";
    }
}
