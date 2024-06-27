using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    public static bool IsPaused { get; private set; } = false;

    private void Start()
    {
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (IsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Home()
    {
        Time.timeScale = 1;
        IsPaused = false;
        SceneManager.LoadScene(1);
    }

    public void Pause()
    {
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
            IsPaused = true;
        }
    }

    public void Resume()
    {
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
            IsPaused = false;
        }
    }

    public void NewGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        int sceneIndex = currentScene.buildIndex;
        SceneManager.LoadScene(sceneIndex);
    }

    public void Settings()
    {
        Debug.Log("Settings");
    }
}
