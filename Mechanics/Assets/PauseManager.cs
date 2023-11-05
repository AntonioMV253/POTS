using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    private bool isPaused;
    public GameObject pausePanel;
    public string mainMenu;
    public Button pauseButton;
    public Button continueButton;
    public Button quitButton;
    public Button resetButton;
    public Button saveButton;

    public List<GameObject> objectsToHideOnPause;
    public GameSaveManager gameSaveManager; // Referencia al GameSaveManager

    void Start()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        pauseButton.onClick.AddListener(ChangePause);
        continueButton.onClick.AddListener(ContinueGame);
        quitButton.onClick.AddListener(QuitToMain);
        resetButton.onClick.AddListener(ResetGame);
        saveButton.onClick.AddListener(SaveGame);
    }

    public void ChangePause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            Time.timeScale = 0f;
            foreach (var obj in objectsToHideOnPause)
            {
                obj.SetActive(false);
            }
        }
        else
        {
            Time.timeScale = 1f;
            foreach (var obj in objectsToHideOnPause)
            {
                obj.SetActive(true);
            }
        }
        pausePanel.SetActive(isPaused);
    }

    public void ContinueGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
        foreach (var obj in objectsToHideOnPause)
        {
            obj.SetActive(true);
        }
    }

    public void QuitToMain()
    {
        SceneManager.LoadScene(mainMenu);
    }

    public void ResetGame()
    {
        gameSaveManager.ResetScriptables();
    }

    public void SaveGame()
    {
        gameSaveManager.SaveScriptables();
    }
}
