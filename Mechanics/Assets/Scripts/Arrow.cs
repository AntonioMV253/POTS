using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float arrowVelocity;
    [SerializeField] private float arrowDamage;
    [SerializeField] private Rigidbody2D rb;

    public float ArrowVelocity
    {
        get { return arrowVelocity; }
        set { arrowVelocity = value; }
    }

    public float ArrowDamage
    {
        get { return arrowDamage; }
        set { arrowDamage = value; }
    }

    private void Start()
    {
        Destroy(gameObject, 4f);
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.up * ArrowVelocity;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            Enemy2 enemy = other.gameObject.GetComponent<Enemy2>();
            if (enemy != null)
            {
                enemy.TakeDamage(ArrowDamage);
                Rigidbody2D enemyRigidbody = other.gameObject.GetComponent<Rigidbody2D>();
                if (enemyRigidbody != null)
                {
                    enemy.Knock(enemyRigidbody, 0.5f, ArrowDamage);
                }
            }
        }
        Destroy(gameObject);
    }
}
