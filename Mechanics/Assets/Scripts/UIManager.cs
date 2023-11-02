using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    private void UpdateUIText()
    {
        textoMonedas.text = TotalMonedas.ToString();
        textoBombas.text = TotalBombas.ToString();
        textoPrisma.text = TotalPrismas.ToString();
    }
}
