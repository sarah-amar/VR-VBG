using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HomeMenuScript : MonoBehaviour
{
    [Header("UI Pages")]

    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject aboutMenu;

    [Header("Main Menu Buttons")]

    public Button startNormalModeButton;
    public Button startDarkModeButton;
    public Button settingsButton;
    public Button aboutButton;
    public Button quitButton;

    public List<Button> goBackButtons;

    void Start()
    {
        EnableMainMenu();
        startNormalModeButton.onClick.AddListener(StartNormalGame);
        startDarkModeButton.onClick.AddListener(StartDarkGame);
        settingsButton.onClick.AddListener(EnableSettings);
        aboutButton.onClick.AddListener(EnableAbout);
        quitButton.onClick.AddListener(QuitGame);

        foreach (var item in goBackButtons)
        {
            item.onClick.AddListener(EnableMainMenu);
        }
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartNormalGame()
    {
        HideAll();
        SceneManager.LoadScene(1);
    }

    public void StartDarkGame()
    {
        HideAll();
         SceneManager.LoadScene(2);
    }

    public void HideAll()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(false);
        aboutMenu.SetActive(false);
    }

    public void EnableMainMenu()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
        aboutMenu.SetActive(false);
    }

    public void EnableSettings()
    {
        print("EnableSettings");
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
        aboutMenu.SetActive(false);
    }

    public void EnableAbout()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(false);
        aboutMenu.SetActive(true);
    }

}
