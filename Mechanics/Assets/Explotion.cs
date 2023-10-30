using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explotion : MonoBehaviour
{
    public float damage = 5f; // Daño de la explosión
    public float destroyDelay = 4f; // Tiempo de espera antes de destruir la explosión
    public float invulnerabilityTime = 1f; // Tiempo de invulnerabilidad después de spawnear la explosión

    public AudioClip destroySound; // Sonido al destruir objetos con tag "Break"

    private bool canCauseDamage = false;

    private void Start()
    {
        StartCoroutine(EnableDamage());
        StartCoroutine(DestroyExplotion(destroyDelay));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (canCauseDamage)
        {
            if (other.gameObject.CompareTag("enemy"))
            {
                // Realiza daño al enemigo
                Enemy2 enemy = other.gameObject.GetComponent<Enemy2>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
            }
            else if (other.gameObject.CompareTag("Break"))
            {
                // Destruye objetos con la etiqueta "Break"
                Destroy(other.gameObject);

                // Reproduce el sonido de destrucción
                AudioSource.PlayClipAtPoint(destroySound, transform.position);
            }
        }
    }

    IEnumerator EnableDamage()
    {
        yield return new WaitForSeconds(invulnerabilityTime);
        canCauseDamage = true;
    }

    IEnumerator DestroyExplotion(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
