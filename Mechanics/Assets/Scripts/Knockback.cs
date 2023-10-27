using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float thrust;
    public float knockTime;
    public float damage;
    public GameObject leftHitbox;
    public GameObject rightHitbox;
    public GameObject downHitbox;
    public GameObject upHitbox;

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

                // Determine which hitbox to activate based on the direction of the knockback.
                float angle = Vector2.SignedAngle(Vector2.up, difference.normalized);

                if (other.gameObject.CompareTag("enemy") && other.isTrigger)
                {
                    hit.GetComponent<Enemy2>().currentState = EnemyState.stagger;
                    other.GetComponent<Enemy2>().Knock(hit, knockTime, damage);
                }
                if (other.gameObject.CompareTag("Player"))
                {
                    if (other.GetComponent<PlayerMovement>().currentState != PlayerState.Stagger)
                    {
                        hit.GetComponent<PlayerMovement>().currentState = PlayerState.Stagger;
                        other.GetComponent<PlayerMovement>().Knock(knockTime, damage);
                    }
                }

                // Activate the appropriate hitbox based on the angle.
                DeactivateAllHitboxes();
                if (angle < -45 && angle >= -135)
                {
                    // Activate left hitbox.
                    leftHitbox.SetActive(true);
                }
                else if (angle >= 45 && angle < 135)
                {
                    // Activate right hitbox.
                    rightHitbox.SetActive(true);
                }
                else if (angle >= 135 || angle < -135)
                {
                    // Activate up hitbox.
                    upHitbox.SetActive(true);
                }
                else
                {
                    // Activate down hitbox.
                    downHitbox.SetActive(true);
                }
            }
        }
    }

    // Helper function to deactivate all hitboxes.
    void DeactivateAllHitboxes()
    {
        leftHitbox.SetActive(false);
        rightHitbox.SetActive(false);
        downHitbox.SetActive(false);
        upHitbox.SetActive(false);
    }
}
