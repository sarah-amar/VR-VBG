using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ActionsGameOver : MonoBehaviour
{
    public Canvas gameOverCanvas;
    public GameObject newGameButton;
    public GameObject quitButton;
    void Start()
    {
        newGameButton.GetComponent<Button>().onClick.AddListener(NewGame);
        quitButton.GetComponent<Button>().onClick.AddListener(QuitGame);
        gameOverCanvas.enabled = false;
    }

    public void NewGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        int sceneIndex = currentScene.buildIndex;
        SceneManager.LoadScene(sceneIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowGameOver()
    {
        gameOverCanvas.enabled = true;
    }
}
