using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBombSpawn : MonoBehaviour
{
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private Transform bombSpawnPoint;
    private InputManager inputManager;
    private float bombCooldown = 3f;
    private float lastBombTime = -3f; // Inicializado en un valor que permite lanzar la primera bomba

    private void Start()
    {
        inputManager = GetComponent <InputManager>();
    }

    private void Update()
    {
        if (inputManager.IsRunButtonHold && Time.time - lastBombTime >= bombCooldown)
        {
            SpawnBomb();
            lastBombTime = Time.time;
        }
    }

    private void SpawnBomb()
    {
        // Instanciar una bomba en la posición del jugador
        Instantiate(bombPrefab, bombSpawnPoint.position, Quaternion.identity);
    }
}
