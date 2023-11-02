using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private int totalMonedas;
    private int totalBombas;
    private int totalPrismas;


    [SerializeField] private TMP_Text textoMonedas;
    [SerializeField] private TMP_Text textoBombas;
    [SerializeField] private TMP_Text textoPrisma;

    private void Start()
    {
        Moneda.sumaMoneda += SumarMonedas;
        Bomba.sumaBomba += SumarBombas;
        Prisma.sumaPrisma += SumarPrismas;

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

    private void SumarPrismas(int prisma)
    {
        totalPrismas += prisma;
        textoPrisma.text = totalPrismas.ToString();
    }

}
