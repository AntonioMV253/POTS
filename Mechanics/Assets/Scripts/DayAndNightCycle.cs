using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayAndNightCycle : MonoBehaviour
{
    [SerializeField] private Gradient lightColor;
    [SerializeField] public GameObject light;
    [SerializeField] private ParticleSystem rainDrops;

    [SerializeField] private AudioClip rainSoundClip;
    [SerializeField] private float rainSoundVolume = 1.0f;
    [SerializeField] private float rainSoundPitch = 1.0f;

    private int days;
    public int Days => days;

    private float time = 50;
    private float nextRainChangeTime;
    private bool isRaining = false;
    private bool isSoundPlaying = false;

    private bool dayChangedFlag = false; // Variable para controlar si el día ya ha sido registrado

    public delegate void OnDayChanged();

    public OnDayChanged DayChanged;

    private AudioSource rainSound;

    private void Start()
    {
        InitializeRainSound();
        UpdateRainState();
        PlayRainSound();
    }

    private void Update()
    {
        if (time > 70)
        {
            time = 0;
        }
        if ((int)time >= 60)
        {
            if (!dayChangedFlag) // Verificar si el día ya ha sido registrado
            {
                DayChanged?.Invoke();
                days++;
                dayChangedFlag = true; // Establecer la bandera de día registrado

                // Añade la lógica de 40% de probabilidad de lluvia en el siguiente día
                if (Random.Range(0f, 1f) < 0.4f)
                {
                    isRaining = true;
                    ShowRainDrops();
                    PlayRainSound();
                }
                else
                {
                    isRaining = false;
                    HideRainDrops();
                    StopRainSound();
                }
            }
        }
        else
        {
            dayChangedFlag = false; // Reiniciar la bandera cuando no se cumple la condición
        }
        time += Time.deltaTime;
        light.GetComponent<Light2D>().color = lightColor.Evaluate(time * 0.002f);
    }

    private void InitializeRainSound()
    {
        rainSound = gameObject.AddComponent<AudioSource>();
        rainSound.clip = rainSoundClip;
        rainSound.volume = rainSoundVolume;
        rainSound.pitch = rainSoundPitch;
        rainSound.loop = true;
    }

    private void PlayRainSound()
    {
        if (rainSound != null && isRaining && !rainSound.isPlaying)
        {
            isSoundPlaying = true;
            rainSound.Play();
        }
    }

    private void StopRainSound()
    {
        if (rainSound != null && (!isRaining || !rainSound.isPlaying))
        {
            isSoundPlaying = false;
            rainSound.Stop();
        }
    }

    private void UpdateRainState()
    {
        if (Time.time >= nextRainChangeTime)
        {
            isRaining = Random.Range(0f, 1f) < 0.9f;
            if (isRaining)
            {
                ShowRainDrops();
                PlayRainSound();
            }
            else
            {
                HideRainDrops();
                StopRainSound();
            }
            nextRainChangeTime = Time.time + Random.Range(60f, 120f);
        }
    }

    private void ShowRainDrops()
    {
        rainDrops.Play();
    }

    private void HideRainDrops()
    {
        rainDrops.Stop();
    }
}
