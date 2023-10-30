using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explotion : MonoBehaviour
{
    public float damage = 5f; // Da�o de la explosi�n
    public float destroyDelay = 4f; // Tiempo de espera antes de destruir la explosi�n
    public float invulnerabilityTime = 1f; // Tiempo de invulnerabilidad despu�s de spawnear la explosi�n

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
                // Realiza da�o al enemigo
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

                // Reproduce el sonido de destrucci�n
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
