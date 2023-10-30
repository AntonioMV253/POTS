using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] public float arrowVelocity;
    [SerializeField] public float arrowDamage;
    [SerializeField] private Rigidbody2D rb;

    private void Start()
    {
        Destroy(gameObject, 4f);
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.up * arrowVelocity;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            Enemy2 enemy = other.gameObject.GetComponent<Enemy2>();
            if (enemy != null)
            {
                if (arrowDamage >= 0)  // Verifica que el daño sea mayor o igual a cero
                {
                    enemy.TakeDamage(arrowDamage);
                }
            }
        }
        Destroy(gameObject);
    }
}
