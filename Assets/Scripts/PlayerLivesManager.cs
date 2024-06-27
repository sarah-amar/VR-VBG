using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLivesManager : MonoBehaviour
{
    public int playerLives = 3;
    public List<Image> heartsImage;
    public Canvas gameOverCanvas;
    public ActionsGameOver actionsGameOver;

    void Start()
    {
        UpdateLivesUI();
        gameOverCanvas.enabled = false;
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
        for (int i = 0; i < heartsImage.Count; i++)
        {
            if (i < playerLives)
            {
                heartsImage[i].enabled = true;
            }
            else
            {
                heartsImage[i].enabled = false;
            }
        }
    }

    void GameOver()
    {
        heartsImage[0].enabled = false;
        actionsGameOver.ShowGameOver();
    }
}
