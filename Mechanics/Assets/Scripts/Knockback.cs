using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Knockback : MonoBehaviour
{
    public float thrust;
    public float knockTime;
    public float damage;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("enemy") || other.gameObject.CompareTag("Player"))
        {
            Rigidbody2D hit = other.GetComponent<Rigidbody2D>();
            if (hit != null)
            {
                Vector2 difference = hit.transform.position - transform.position;
                difference = difference.normalized * thrust;
                hit.AddForce(difference, ForceMode2D.Impulse);
                if (other.gameObject.CompareTag("enemy") && other.isTrigger){
                    hit.GetComponent<Enemy2>().currentState = EnemyState.stagger;
                    other.GetComponent<Enemy2>().Knock(hit, knockTime, damage);
                }
                if (other.gameObject.CompareTag("Player"))
                {
                    hit.GetComponent<PlayerMovement>().currentState = PlayerState.Stagger;
                    other.GetComponent<PlayerMovement>().Knock(knockTime);
                }
            } 
        }
    }
}

