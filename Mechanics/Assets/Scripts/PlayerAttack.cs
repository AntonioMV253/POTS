using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] SpriteRenderer arrowGFX;
    [SerializeField] Transform bow;
    [SerializeField] Transform arrowSpawnPoint;
    private PlayerMovement playerMovement; // Para detectar la dirección del jugador

    [Range(0, 10)]
    [SerializeField] float bowPower;

    float cooldown = 0f;
    bool canFire = true;

    private InputManager inputManager;

    private void Start()
    {
        inputManager = FindObjectOfType<InputManager>();
        playerMovement = GetComponent<PlayerMovement>(); // Inicializamos playerMovement
    }

    private void Update()
    {
        HandleBowPositionAndRotation();

        if (inputManager.IsActionButtonHold && canFire)
        {
            FireBow();
        }
        else
        {
            if (cooldown > 0f)
            {
                cooldown -= Time.deltaTime;
            }
            else
            {
                canFire = true;
            }
        }
    }

    void HandleBowPositionAndRotation()
    {
        if (playerMovement != null && playerMovement.moveInput != Vector2.zero)
        {
            Vector2 playerDirection = playerMovement.moveInput.normalized;
            float rotationZ = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;
            bow.rotation = Quaternion.Euler(0f, 0f, rotationZ);

            Vector3 bowOffset = CalculateBowOffset(playerDirection);
            bow.position = transform.position + bowOffset;
        }
    }

    private Vector3 CalculateBowOffset(Vector2 playerDirection)
    {
        float xOffset = 0.5f * playerDirection.x;
        float yOffset = 0.5f * playerDirection.y;

        if (Mathf.Abs(playerDirection.x) > Mathf.Abs(playerDirection.y))
        {
            yOffset = 0; 
        }
        else
        {
            xOffset = 0;
        }

        return new Vector3(xOffset, yOffset, 0);
    }

    void FireBow()
    {
        float arrowSpeed = bowPower;
        float arrowDamage = bowPower;

        Quaternion playerRotation = bow.rotation;
        Quaternion rot = playerRotation * Quaternion.Euler(new Vector3(0f, 0f, -90f));

        GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, rot);
        Arrow arrowScript = arrow.GetComponent<Arrow>();
        arrowScript.ArrowVelocity = arrowSpeed;
        arrowScript.ArrowDamage = arrowDamage;

        canFire = false;
        cooldown = 1.5f; 
    }
}
