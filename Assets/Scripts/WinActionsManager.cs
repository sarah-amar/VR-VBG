using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class WinActionsManager : MonoBehaviour
{
    public Canvas winCanvas;
    public GameObject newGameButton;
    public GameObject quitButton;
    public GameObject player;
    public ButtonInteractionHandler movementHandler;
    private ContinuousMoveProviderBase[] moveProviders;
    private ContinuousTurnProviderBase[] turnProviders;
    private GetAllGameItems gameItemsManager;
    public Timer gameTimer;

    void Start()
    {
        newGameButton.GetComponent<Button>().onClick.AddListener(NewGame);
        quitButton.GetComponent<Button>().onClick.AddListener(QuitGame);
        winCanvas.enabled = false;

        moveProviders = FindObjectsOfType<ContinuousMoveProviderBase>();
        turnProviders = FindObjectsOfType<ContinuousTurnProviderBase>();

        gameItemsManager = FindObjectOfType<GetAllGameItems>();
    }

    public void NewGame()
    {
        if (gameItemsManager != null)
        {
            gameItemsManager.ResetCollectedItemsCount();
        }

        SceneManager.LoadScene(1);
    }

    public void HideWin()
    {
        winCanvas.enabled = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowWin()
    {
        winCanvas.enabled = true;
        DisableMovement();
        if (gameTimer != null)
        {
            gameTimer.StopTimer();
        }
    }

    private void DisableMovement()
    {
        if (movementHandler != null)
        {
            movementHandler.enabled = false;
        }

        foreach (var moveProvider in moveProviders)
        {
            moveProvider.enabled = false;
        }

        foreach (var turnProvider in turnProviders)
        {
            turnProvider.enabled = false;
        }
    }
}
