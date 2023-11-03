using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MenuInicial : MonoBehaviour
{
    public FloatValue currentHealth; // Asigna la variable actual en el Inspector con la referencia correcta.

    // Aseg�rate de asignar el valor inicial de la vida en el Inspector
    public float initialHealthValue = 6.0f;

    private void Start()
    {
        // Cuando el men� inicial comienza, establece la vida al valor inicial
        currentHealth.RuntimeValue = initialHealthValue;
    }

    public void Jugar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Salir()
    {
        Debug.Log("Salir");
        Application.Quit();
    }

    public void OnActionButton(InputValue input)
    {
        // Esta funci�n se llama cuando se presiona el bot�n de acci�n
        Jugar(); // Llama a la funci�n Jugar() cuando se presiona el bot�n de acci�n
    }
}
