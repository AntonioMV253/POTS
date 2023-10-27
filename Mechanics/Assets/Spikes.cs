using UnityEngine;

public class Spikes : MonoBehaviour
{
    private Animator anim;
    public float damage = 1.0f;  // Da�o que los pinchos le har�n al jugador
    public float knockTime = 1.0f;  // Tiempo que el jugador estar� en estado stagger

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Activa la animaci�n de pinchos activados
            anim.SetTrigger("activoPincho");

            // Obtiene el script de movimiento del jugador y le hace da�o
            PlayerMovement player = collision.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player.Knock(knockTime, damage);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Regresa a la animaci�n de pinchos desactivados
            anim.SetTrigger("pinchos");
        }
    }
}
