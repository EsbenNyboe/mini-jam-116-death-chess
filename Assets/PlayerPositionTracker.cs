using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionTracker : MonoBehaviour
{
    public static PlayerPositionTracker Instance;
    public Vector2Int playerGridPosition;

    private GameGridScript _gameGridScript;

    private void Awake()
    {
        Instance = this;
        _gameGridScript = FindObjectOfType<GameGridScript>();
    }

    private void Update()
    {
        playerGridPosition = _gameGridScript.GetGridPosFromWorld(transform.position);
    }
}