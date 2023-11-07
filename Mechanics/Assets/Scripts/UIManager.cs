using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public int TotalMonedas { get; private set; }
    public int TotalBombas { get; private set; }
    public int TotalPrismas { get; private set; }

    public static UIManager Instance { get; private set; }

    [SerializeField] private TMP_Text textoMonedas;
    [SerializeField] private TMP_Text textoBombas;
    [SerializeField] private TMP_Text textoPrisma;

    public GameObject uiObject; // Asigna tu objeto de UI desde el Inspector

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += OnSceneLoaded; // Suscribe el método OnSceneLoaded al evento sceneLoaded
    }

    private void Start()
    {
        Moneda.sumaMoneda += SumarMonedas;
        Bomba.sumaBomba += SumarBombas;
        Prisma.sumaPrisma += SumarPrismas;

        UpdateUIText();
    }

    private void SumarMonedas(int moneda)
    {
        TotalMonedas += moneda;
        UpdateUIText();
    }

    private void SumarBombas(int bomba)
    {
        TotalBombas += bomba;
        UpdateUIText();
    }

    private void SumarPrismas(int prisma)
    {
        TotalPrismas += prisma;
        UpdateUIText();
    }

    public void RestarBombas()
    {
        if (TotalBombas > 0)
        {
            TotalBombas--;
            UpdateUIText();
        }
    }

    private void UpdateUIText()
    {
        textoMonedas.text = TotalMonedas.ToString();
        textoBombas.text = TotalBombas.ToString();
        textoPrisma.text = TotalPrismas.ToString();
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
