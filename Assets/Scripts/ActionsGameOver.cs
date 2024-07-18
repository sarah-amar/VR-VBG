using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class ActionsGameOver : MonoBehaviour
{
    public Canvas gameOverCanvas;
    public GameObject newGameButton;
    public GameObject quitButton;
    public GameObject player;
    public ButtonInteractionHandler movementHandler; // Référence au script de gestion des déplacements
    private ContinuousMoveProviderBase[] moveProviders;
    private ContinuousTurnProviderBase[] turnProviders;
    private GetAllGameItems gameItemsManager; // Référence au script GetAllGameItems
    public Timer gameTimer;

    void Start()
    {
        newGameButton.GetComponent<Button>().onClick.AddListener(NewGame);
        quitButton.GetComponent<Button>().onClick.AddListener(QuitGame);
        gameOverCanvas.enabled = false;

        // Trouver tous les ContinuousMoveProvider et ContinuousTurnProvider dans la scène
        moveProviders = FindObjectsOfType<ContinuousMoveProviderBase>();
        turnProviders = FindObjectsOfType<ContinuousTurnProviderBase>();

        // Trouver le script GetAllGameItems dans la scène
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

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowGameOver()
    {
        gameOverCanvas.enabled = true;
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
