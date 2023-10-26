using System.Collections;
using UnityEngine;

public class PickUpableItem : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isHeld = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true; // Desactiva la f�sica para que el objeto no caiga autom�ticamente
    }

    public void PickUp()
    {
        isHeld = true;
        rb.isKinematic = true; // Desactiva la f�sica mientras se sostiene el objeto
    }

    public void Throw(Vector3 direction, float throwForce)
    {
        isHeld = false;
        rb.isKinematic = false; // Activa la f�sica al lanzar el objeto

        // Aplica una fuerza para lanzar el objeto en la direcci�n dada
        rb.velocity = direction.normalized * throwForce;
    }

    public bool IsHeld()
    {
        return isHeld;
    }
}
