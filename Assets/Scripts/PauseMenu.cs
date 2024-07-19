using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    public static bool IsPaused { get; private set; } = false;
    private ContinuousMoveProviderBase[] moveProviders;
    private ContinuousTurnProviderBase[] turnProviders;

    private void Start()
    {
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }

        moveProviders = FindObjectsOfType<ContinuousMoveProviderBase>();
        turnProviders = FindObjectsOfType<ContinuousTurnProviderBase>();
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
            DisableMovement();
        }
    }

    public void Resume()
    {
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
            IsPaused = false;
            EnableMovement();
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
        // Debug.Log("Settings");
    }

    private void DisableMovement()
    {
        foreach (var moveProvider in moveProviders)
        {
            moveProvider.enabled = false;
        }

        foreach (var turnProvider in turnProviders)
        {
            turnProvider.enabled = false;
        }
    }

    private void EnableMovement()
    {
        foreach (var moveProvider in moveProviders)
        {
            moveProvider.enabled = true;
        }

        foreach (var turnProvider in turnProviders)
        {
            turnProvider.enabled = true;
        }
    }
}
