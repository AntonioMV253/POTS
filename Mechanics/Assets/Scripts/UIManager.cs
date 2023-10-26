using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    private int totalMonedas;
    [SerializeField] private TMP_Text textoMonedas;
    [SerializeField] private Sprite corazonDesactivado;
    private void Start()
    {
        Moneda.sumaMoneda += SumarMonedas;
    }
    private void SumarMonedas(int moneda)
    {
        totalMonedas += moneda;
        textoMonedas.text = totalMonedas.ToString();
    }

}