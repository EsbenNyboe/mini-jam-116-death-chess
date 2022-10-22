using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    private Scene scene;

    private void Awake()
    {
        scene = SceneManager.GetActiveScene();
    }

    public void PlayGame()
    {
        Debug.Log("Play Game!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame()
    {
        Debug.Log("Exit Game!");
        Application.Quit();
    }

    void OnSceneLoaded(Scene scene)
    {
        if (scene.name == "MainMenu")
        {
            Time.timeScale = 0.01f;
            Time.fixedDeltaTime = 0.0001f;
        }

        else
        {
            Time.timeScale = 1f;
            Time.fixedDeltaTime = 0.02f;
        }
    }
}
