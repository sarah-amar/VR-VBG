using UnityEngine;
using UnityEngine.UI;

public class PlayerLivesManager : MonoBehaviour
{
    public int playerLives = 3;
    public Text livesText;

    void Start()
    {
        UpdateLivesUI();
    }

    public void LoseLife()
    {
        playerLives--;

        if (playerLives <= 0)
        {
            GameOver();
        }
        else
        {
            UpdateLivesUI();
        }
    }

    public void WinLife()
    {
        if(playerLives < 3)
        {
            playerLives++;
            UpdateLivesUI();
        }
    }

    void UpdateLivesUI()
    {
        if (livesText != null)
        {
            livesText.text = "Lives: " + playerLives;
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over!");
    }
}
