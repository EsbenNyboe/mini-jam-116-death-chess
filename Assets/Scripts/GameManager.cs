using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        Application.targetFrameRate = 60;

        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
    }
}
