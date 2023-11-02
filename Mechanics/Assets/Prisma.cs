using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prisma : MonoBehaviour
{
    public delegate void SumaPrisma(int cantidadPrisma);
    public static event SumaPrisma sumaPrisma;

    [SerializeField] private int cantidadPrisma = 1;
    [SerializeField] private AudioClip soundClip;

    // Dentro de Bomba:
    private bool hasBeenCollected = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasBeenCollected && collision.gameObject.CompareTag("Player"))
        {
            if (sumaPrisma != null)
            {
                SumarPrisma();
                hasBeenCollected = true;
                Destroy(this.gameObject);
            }
        }
    }


    private void SumarPrisma()
    {
        if (soundClip != null)
        {
            AudioSource.PlayClipAtPoint(soundClip, transform.position);
        }

        sumaPrisma?.Invoke(cantidadPrisma);
    }
}
