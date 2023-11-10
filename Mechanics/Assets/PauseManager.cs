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
    public GameObject deathPanel; // Asegúrate de que este panel está asignado en el Inspector.

    public List<GameObject> objectsToHideOnPause;
    public GameSaveManager gameSaveManager;
    public Transform savedPlayerTransform;
    public GameObject uiObject;
    public List<GameObject> objectsToActivateOnStart;

    void Awake()
    {
        // Evita la duplicación de este objeto en la carga de la escena.
        GameObject[] objs = GameObject.FindGameObjectsWithTag(gameObject.tag);
        if (objs.Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }

    void Start()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        if (pauseButton != null)
        {
            pauseButton.onClick.AddListener(ChangePause);
        }
        if (continueButton != null)
        {
            continueButton.onClick.AddListener(ContinueGame);
        }

        if (uiObject != null)
        {
            uiObject.SetActive(true);
        }

        if (objectsToActivateOnStart != null)
        {
            foreach (var obj in objectsToActivateOnStart)
            {
                obj.SetActive(true);
            }
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Desactiva los paneles al cargar una nueva escena
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }

        if (deathPanel != null)
        {
            deathPanel.SetActive(false);
        }

        // Restablece el estado del juego si estaba pausado
        isPaused = false;
        Time.timeScale = 1f;

        // Restablece las referencias necesarias, como la del jugador
        savedPlayerTransform = null;

        // Activa cualquier objeto UI necesario para la nueva escena
        if (scene.name != "MenuInicial" && uiObject != null)
        {
            uiObject.SetActive(true);
        }
        else if (uiObject != null)
        {
            uiObject.SetActive(false);
        }
    }

    public void ChangePause()
    {
        isPaused = !isPaused;
        if (pausePanel != null)
        {
            pausePanel.SetActive(isPaused);
            Time.timeScale = isPaused ? 0f : 1f;
        }

        foreach (var obj in objectsToHideOnPause)
        {
            if (obj != null)
                obj.SetActive(!isPaused);
        }
    }

    public void ActivateDeathPanel()
    {
        if (deathPanel != null)
        {
            deathPanel.SetActive(true); // Activa el panel de muerte
            Time.timeScale = 0f; // Pausa el juego
            isPaused = true;
        }
        else
        {
            Debug.LogError("Death panel not assigned.");
        }
    }

    public void DeactivateDeathPanel()
    {
        if (deathPanel != null)
        {
            deathPanel.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
        }
        else
        {
            Debug.LogError("Death panel not assigned.");
        }
    }

    public void ContinueGame()
    {
        ChangePause();
    }

    public void QuitToMain()
    {
        SceneManager.LoadScene(mainMenu);
        Time.timeScale = 1f;
    }
}
