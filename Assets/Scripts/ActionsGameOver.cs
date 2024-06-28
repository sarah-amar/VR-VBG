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
    public GameObject player; // Référence au joueur
    public ButtonInteractionHandler movementHandler; // Référence au script de gestion des déplacements
    private ContinuousMoveProviderBase[] moveProviders;
    private ContinuousTurnProviderBase[] turnProviders;
    private GetAllGameItems gameItemsManager; // Référence au script GetAllGameItems

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
            gameItemsManager.ResetCollectedItemsCount(); // Réinitialiser le compteur d'objets collectés
        }

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
        DisableMovement();
    }

    private void DisableMovement()
    {
        if (movementHandler != null)
        {
            movementHandler.enabled = false;
        }

        // Désactiver tous les ContinuousMoveProvider
        foreach (var moveProvider in moveProviders)
        {
            moveProvider.enabled = false;
        }

        // Désactiver tous les ContinuousTurnProvider
        foreach (var turnProvider in turnProviders)
        {
            turnProvider.enabled = false;
        }
    }
}
