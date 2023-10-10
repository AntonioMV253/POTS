using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayAndNightCycle : MonoBehaviour
{
    [SerializeField] private Gradient lightColor;
    [SerializeField] public GameObject light;
    [SerializeField] private ParticleSystem rainDrops; // Referencia al sistema de part�culas

    private int days;
    public int Days => days;

    private float time = 50;

    private bool canChangeDay = true;

    public delegate void OnDayChanged();

    public OnDayChanged DayChanged;

    // Tiempo aleatorio para ocultar/mostrar el sistema de part�culas
    private float randomParticleToggleTime = 0f;
    private float particleToggleInterval = 10f; // Intervalo de tiempo para cambiar las part�culas (en segundos)

    private void Start()
    {
        // Inicialmente, mostramos el sistema de part�culas
        ShowRainDrops();
    }

    private void Update()
    {
        if (time > 500)
        {
            time = 0;
        }
        if ((int)time == 250 && canChangeDay)
        {
            canChangeDay = false;
            DayChanged();
            days++;
        }
        if ((int)time == 255)
            canChangeDay = true;
        time += Time.deltaTime;
        light.GetComponent<Light2D>().color = lightColor.Evaluate(time * 0.002f);

        // Actualizar el temporizador para ocultar/mostrar part�culas
        randomParticleToggleTime += Time.deltaTime;
        if (randomParticleToggleTime >= particleToggleInterval)
        {
            // Cambiar el estado de las part�culas aleatoriamente
            if (Random.value < 0.5f)
            {
                ShowRainDrops();
            }
            else
            {
                HideRainDrops();
            }

            // Reiniciar el temporizador aleatorio
            randomParticleToggleTime = 0f;
        }
    }

    // Funci�n para mostrar las part�culas
    private void ShowRainDrops()
    {
        rainDrops.Play(); // Iniciar el sistema de part�culas
    }

    // Funci�n para ocultar las part�culas
    private void HideRainDrops()
    {
        rainDrops.Stop(); // Detener el sistema de part�culas
    }
}
