using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Knockback : MonoBehaviour
{

    [SerializeField] private float thrust;
    [SerializeField] private float knockTime;
    [SerializeField] private string otherTag;
    public float damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        /*
        if (other.gameObject.CompareTag("breakable") && this.gameObject.CompareTag("Player"))
        {
            other.GetComponent<pot>().Smash();
        }
        */
        if (other.gameObject.CompareTag(otherTag) && other.isTrigger)
        {
            Rigidbody2D hit = other.GetComponentInParent<Rigidbody2D>();
            if (hit != null)
            {
                Vector3 difference = hit.transform.position - transform.position;
                difference = difference.normalized * thrust;
                hit.DOMove(hit.transform.position + difference, knockTime);
                //hit.AddForce(difference, ForceMode2D.Impulse);

                if (other.gameObject.CompareTag("enemy") && other.isTrigger)
                {
                    hit.GetComponent<Enemy2>().currentState = EnemyState.stagger;
                    other.GetComponent<Enemy2>().Knock(hit, knockTime, damage);
                }

                if (other.GetComponentInParent<PlayerMovement>() != null && other.GetComponentInParent<PlayerMovement>().currentState != PlayerState.Stagger)
                {
                    hit.GetComponent<PlayerMovement>().currentState = PlayerState.Stagger;
                    other.GetComponent<PlayerMovement>().Knock(knockTime, damage);
                }


            }
        }
    }


}