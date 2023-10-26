using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomba : MonoBehaviour
{
    public delegate void SumaBomba(int cantidadBombas);
    public static event SumaBomba sumaBomba;

    [SerializeField] private int cantidadBombas = 1;
    [SerializeField] private AudioClip soundClip;

    // Dentro de Bomba:
    private bool hasBeenCollected = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasBeenCollected && collision.gameObject.CompareTag("Player"))
        {
            if (sumaBomba != null)
            {
                SumarBomba();
                hasBeenCollected = true;
                Destroy(this.gameObject);
            }
        }
    }


    private void SumarBomba()
    {
        if (soundClip != null)
        {
            AudioSource.PlayClipAtPoint(soundClip, transform.position);
        }

        sumaBomba?.Invoke(cantidadBombas);
    }
}
