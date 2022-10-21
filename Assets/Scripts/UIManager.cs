using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [FormerlySerializedAs("_scoreText")] [SerializeField]
    Text scoreText;

    [FormerlySerializedAs("_timeText")] [SerializeField]
    Text healthText;

    private int _score;
    [SerializeField] private int lives = 10;
    private int _currentLives;

    private bool _gameOver;

    private PlayerControls _player;

    private float _gameOverSinkSpeed;

    private float _respawnTimer;

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
        _currentLives = lives;
        _player = FindObjectOfType<PlayerControls>();
    }

    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        scoreText.text = "Score: " + _score;
        healthText.text = "Lives: " + _currentLives;

        if (_gameOver)
        {
            _gameOverSinkSpeed -= 0.00001f;
            if (_gameOverSinkSpeed < 0)
            {
                _gameOverSinkSpeed = 0;
            }

            _player.gameObject.transform.localPosition -= new Vector3(0, _gameOverSinkSpeed, 0);
            _respawnTimer += Time.deltaTime;
            if (_respawnTimer > 3f)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _score = 0;
                    _currentLives = lives;
                    _gameOver = false;
                    _respawnTimer = 0;
                    SceneManager.LoadScene(0);
                }
            }
        }
    }

    public void AddToScore(int points)
    {
        _score += points;
    }

    public void TakeDamage(int points)
    {
        if (_currentLives == 1)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/GameOver", transform.position);

            _gameOver = true;
            _gameOverSinkSpeed = 0.005f;
            _player.enabled = false;
        }

        _currentLives -= points;
        if (_currentLives < 0)
        {
            _currentLives = 0;
        }
    }
}