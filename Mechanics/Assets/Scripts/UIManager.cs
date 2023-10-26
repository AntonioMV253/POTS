using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private int totalMonedas;
    private int totalBombas;

    [SerializeField] private TMP_Text textoMonedas;
    [SerializeField] private TMP_Text textoBombas;

    private void Start()
    {
        Moneda.sumaMoneda += SumarMonedas;
        Bomba.sumaBomba += SumarBombas; 
    }

    private void SumarMonedas(int moneda)
    {
        totalMonedas += moneda;
        textoMonedas.text = totalMonedas.ToString();
    }

    private void SumarBombas(int bomba)
    {
        totalBombas += bomba;
        textoBombas.text = totalBombas.ToString();
    }
}
