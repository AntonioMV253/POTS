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
        SceneManager.sceneLoaded += OnSceneLoaded; // Suscribe el método OnSceneLoaded al evento sceneLoaded
    }

    // Este método se ejecutará cada vez que se cargue una nueva escena
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Verifica si la escena recién cargada no es el menú principal
        if (scene.name != "MenuInicial")
        {
            // Activa tu objeto de UI en todas las escenas excepto el menú principal
            if (uiObject != null)
            {
                uiObject.SetActive(true);
            }
        }
        else
        {
            // Desactiva tu objeto de UI en el menú principal
            if (uiObject != null)
            {
                uiObject.SetActive(false);
            }
        }
    }
}
