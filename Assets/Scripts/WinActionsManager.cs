using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinActionsManager : MonoBehaviour
{
    public Canvas winCanvas;
    public GameObject newGameButton;
    public GameObject quitButton;

    void Start()
    {
        newGameButton.GetComponent<Button>().onClick.AddListener(NewGame);
        quitButton.GetComponent<Button>().onClick.AddListener(QuitGame);
    }

    public void ShowWin()
    {
        winCanvas.enabled = true;
    }

    public void HideWin()
    {
        winCanvas.enabled = false;
    }

    public void NewGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        int sceneIndex = currentScene.buildIndex + 1;
        SceneManager.LoadScene(sceneIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
