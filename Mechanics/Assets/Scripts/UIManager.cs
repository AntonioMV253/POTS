using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public int TotalMonedas { get; private set; }
    public int TotalBombas { get; private set; }
    public int TotalPrismas { get; private set; }

    [SerializeField] private TMP_Text textoMonedas;
    [SerializeField] private TMP_Text textoBombas;
    [SerializeField] private TMP_Text textoPrisma;

    public GameObject uiObject; // Asigna tu objeto de UI desde el Inspector
    public GameObject ganarPanel; // Asigna el GanarPanel desde el Inspector

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
            return;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnEnable()
    {
        Moneda.sumaMoneda += SumarMonedas; // Suscribe tus métodos a los eventos
        Bomba.sumaBomba += SumarBombas;
        Prisma.sumaPrisma += SumarPrismas;
    }

    private void OnDisable()
    {
        Moneda.sumaMoneda -= SumarMonedas; // Desuscribe tus métodos de los eventos
        Bomba.sumaBomba -= SumarBombas;
        Prisma.sumaPrisma -= SumarPrismas;
    }

    private void Start()
    {
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

        if (TotalPrismas >= 3)
        {
            MostrarGanarPanel();
        }
    }

    private void MostrarGanarPanel()
    {
        if (ganarPanel != null)
        {
            ganarPanel.SetActive(true);
        }
        else
        {
            Debug.LogError("GanarPanel no está asignado en el Inspector.");
        }
    }

    public void RestarBombas()
    {
        if (TotalBombas > 0)
        {
            TotalBombas--;
            UpdateUIText();
        }
    }

    public void ResetCounts()
    {
        TotalMonedas = 0;
        TotalBombas = 0;
        TotalPrismas = 0;
        UpdateUIText();
    }

    private void UpdateUIText()
    {
        textoMonedas.text = TotalMonedas.ToString();
        textoBombas.text = TotalBombas.ToString();
        textoPrisma.text = TotalPrismas.ToString();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ResetCounts(); // Reinicia las cuentas al cargar una escena

        if (scene.name != "MenuInicial" && uiObject != null)
        {
            uiObject.SetActive(true);
        }
        else if (uiObject != null)
        {
            uiObject.SetActive(false);
        }
    }
}
