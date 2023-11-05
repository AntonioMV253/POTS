using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;

    public GameObject uiObject; // Asigna tu objeto de UI desde el Inspector

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // Suscribe el m�todo OnSceneLoaded al evento sceneLoaded
    }

    // Este m�todo se ejecutar� cada vez que se cargue una nueva escena
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Verifica si la escena reci�n cargada no es el men� principal
        if (scene.name != "MenuInicial")
        {
            // Activa tu objeto de UI en todas las escenas excepto el men� principal
            if (uiObject != null)
            {
                uiObject.SetActive(true);
            }
        }
        else
        {
            // Desactiva tu objeto de UI en el men� principal
            if (uiObject != null)
            {
                uiObject.SetActive(false);
            }
        }
    }
}
