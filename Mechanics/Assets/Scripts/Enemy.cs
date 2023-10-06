//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Enemy : MonoBehaviour
//{
//    [Header("Movement")]
//    public float speed = 3f;
//    [Header("Attack")]
//    [SerializeField] private float attackDamage = 10f;
//    [SerializeField] private float attackSpeed = 1f;
//    private float canAttack;

//    [Header("Health")]
//    private float health;
//    [SerializeField] private float maxHealth;

//    private Transform target;
//    private SpriteRenderer spriteRenderer; // Referencia al SpriteRenderer

//    private void Start()
//    {
//        health = maxHealth;
//        spriteRenderer = GetComponent<SpriteRenderer>(); // Obtén la referencia al SpriteRenderer
//    }

//    public void TakeDamage(float dmg)
//    {
//        health -= dmg;
//        Debug.Log("Enemy Health: " + health);

//        if (health <= 0)
//        {
//            Destroy(gameObject);
//        }
//    }

//    private void FixedUpdate()
//    {
//        if (target != null)
//        {
//            float step = speed * Time.deltaTime;
//            transform.position = Vector2.MoveTowards(transform.position, target.position, step);

//            // Determina la dirección en la que se encuentra el jugador
//            if (target.position.x < transform.position.x)
//            {
//                // Si el jugador está a la izquierda, rota el SpriteRenderer en el eje X
//                spriteRenderer.flipX = true;
//            }
//            else
//            {
//                // Si el jugador está a la derecha, no rotes el SpriteRenderer
//                spriteRenderer.flipX = false;
//            }

//            if (attackSpeed <= canAttack)
//            {
//                Attack();
//                canAttack = 0f;
//            }
//            else
//            {
//                canAttack += Time.deltaTime;
//            }
//        }
//    }

//    private void Attack()
//    {
//        // Causa herida al jugador
//        PlayerMovement playerMovement = target.GetComponent<PlayerMovement>();
//        if (playerMovement != null)
//        {
//            playerMovement.CausarHerida();
//        }
//    }

//    private void OnTriggerEnter2D(Collider2D other)
//    {
//        if (other.gameObject.tag == "Player")
//        {
//            target = other.transform;
//        }
//    }

//    private void OnTriggerExit2D(Collider2D other)
//    {
//        if (other.gameObject.tag == "Player")
//        {
//            target = null;
//        }
//    }
//}
