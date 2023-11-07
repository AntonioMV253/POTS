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

    public List<GameObject> objectsToHideOnPause;
    public GameSaveManager gameSaveManager;
    public Transform savedPlayerTransform;

    // Agrega un campo para tu UI o la lista de objetos
    public GameObject uiObject; // Asigna tu objeto de UI desde el Inspector
    public List<GameObject> objectsToActivateOnStart; // Asigna tus objetos desde el Inspector

    void Awake()
    {
        // Asegúrate de que el PauseManager sea un objeto DontDestroyOnLoad
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        pauseButton.onClick.AddListener(ChangePause);
        continueButton.onClick.AddListener(ContinueGame);

        // Activa tu UI al inicio
        if (uiObject != null)
        {
            uiObject.SetActive(true);
        }

        // Activa una lista de objetos al inicio
        if (objectsToActivateOnStart != null)
        {
            foreach (var obj in objectsToActivateOnStart)
            {
                obj.SetActive(true);
            }
        }
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
            transform.position = savedPlayerTransform.position;
            transform.rotation = savedPlayerTransform.rotation;
            transform.localScale = savedPlayerTransform.localScale;

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
        transform.position = savedPlayerTransform.position;
        transform.rotation = savedPlayerTransform.rotation;
        transform.localScale = savedPlayerTransform.localScale;
        pausePanel.SetActive(false);
        foreach (var obj in objectsToHideOnPause)
        {
            obj.SetActive(true);
        }
    }

    public void QuitToMain()
    {
        SceneManager.LoadScene(mainMenu);
        Time.timeScale = 1f;
    }
}
