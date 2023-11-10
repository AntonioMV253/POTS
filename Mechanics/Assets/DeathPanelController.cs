using UnityEngine;

public class DeathPanelController : MonoBehaviour
{
    // Aseg�rate de que el panel est� desactivado por defecto
    // en el Inspector de Unity.
    void Awake()
    {
        gameObject.SetActive(false);
    }

    public void ActivateDeathPanel()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;

    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }
}
