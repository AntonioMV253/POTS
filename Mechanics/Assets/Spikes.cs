using UnityEngine;

public class Spikes : MonoBehaviour
{
    private Animator anim;
    public float damage = 1.0f;  // Daño que los pinchos le harán al jugador
    public float knockTime = 1.0f;  // Tiempo que el jugador estará en estado stagger

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Activa la animación de pinchos activados
            anim.SetTrigger("activoPincho");

            // Obtiene el script de movimiento del jugador y le hace daño
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
            // Regresa a la animación de pinchos desactivados
            anim.SetTrigger("pinchos");
        }
    }
}
