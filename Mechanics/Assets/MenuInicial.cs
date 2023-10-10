using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MenuInicial : MonoBehaviour
{
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
        // Esta función se llama cuando se presiona el botón de acción
        Jugar(); // Llama a la función Jugar() cuando se presiona el botón de acción
    }
}
