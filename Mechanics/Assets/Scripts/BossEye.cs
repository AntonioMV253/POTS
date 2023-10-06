//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class BossEye : MonoBehaviour
//{
//    public float chargeSpeed = 5f;
//    public float chargeDuration = 1f;
//    public float chargeCooldown = 10f;
//    private Vector2 chargeDirection;
//    private float chargeTimer;
//    private bool isCharging = false;

//    public float maxHealth = 100f;
//    private float health;

//    public GameObject enemyPrefab;
//    public Transform spawnPoint;
//    public int maxSpawnCount = 5;
//    private int spawnCount = 0;
//    public float spawnInterval = 10f;
//    public float spawnRange = 10f;
//    private Transform playerTransform;

//    private bool playerInTrigger = false;
//    private float initialSpeed;

//    private void Start()
//    {
//        health = maxHealth;
//        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
//        initialSpeed = chargeSpeed;
//        StartCoroutine(SpawnEnemies());
//    }

//    private void Update()
//    {
//        if (playerInTrigger && !isCharging)
//        {
//            Vector2 directionToPlayer = (playerTransform.position - transform.position).normalized;
//            chargeDirection = new Vector2(directionToPlayer.x, directionToPlayer.y).normalized;

//            isCharging = true;
//            chargeSpeed = initialSpeed * 2;
//            chargeTimer = chargeDuration;
//        }

//        if (isCharging)
//        {
//            transform.Translate(chargeDirection * chargeSpeed * Time.deltaTime);
//            chargeTimer -= Time.deltaTime;
//            if (chargeTimer <= 0f)
//            {
//                isCharging = false;
//                chargeSpeed = initialSpeed;
//                StartCoroutine(ChargeCooldown());
//            }
//        }

//        if (playerInTrigger)
//        {
//            Attack();
//        }
//    }

//    private void OnTriggerEnter2D(Collider2D other)
//    {
//        if (other.gameObject.CompareTag("Player"))
//        {
//            playerInTrigger = true;
//        }
//        else if (other.gameObject.CompareTag("Arrow"))
//        {
//            TakeDamage(10f);
//            Destroy(other.gameObject);
//        }
//    }

//    private void OnTriggerExit2D(Collider2D other)
//    {
//        if (other.gameObject.CompareTag("Player"))
//        {
//            playerInTrigger = false;
//        }
//    }

//    private void Attack()
//    {
//        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
//        if (distanceToPlayer < 2f)
//        {
//            PlayerMovement playerMovement = playerTransform.GetComponent<PlayerMovement>();
//            if (playerMovement != null)
//            {
//                playerMovement.CausarHerida();
//            }
//        }
//    }

//    private void TakeDamage(float damage)
//    {
//        health -= damage;
//        if (health <= 0)
//        {
//            Destroy(gameObject);
//        }
//    }

//    private IEnumerator SpawnEnemies()
//    {
//        while (true)
//        {
//            if (spawnCount < maxSpawnCount && playerInTrigger)
//            {
//                if (enemyPrefab != null && spawnPoint != null)
//                {
//                    Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
//                    spawnCount++;
//                }
//            }

//            yield return new WaitForSeconds(spawnInterval);
//        }
//    }

//    private IEnumerator ChargeCooldown()
//    {
//        yield return new WaitForSeconds(chargeCooldown);
//        chargeTimer = 0f;
//    }
//}
