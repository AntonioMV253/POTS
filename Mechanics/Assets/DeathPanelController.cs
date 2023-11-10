using UnityEngine;

public class DeathPanelController : MonoBehaviour
{
    // Asegúrate de que el panel esté desactivado por defecto
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
