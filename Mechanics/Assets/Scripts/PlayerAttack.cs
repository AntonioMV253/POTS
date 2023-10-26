using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] SpriteRenderer arrowGFX;
    [SerializeField] Slider bowPowerSlider;
    [SerializeField] Transform bow;
    [SerializeField] Transform arrowSpawnPoint;
    private PlayerMovement playerMovement; // Para detectar la dirección del jugador

    [Range(0, 10)]
    [SerializeField] float bowPower;

    [Range(0, 3)]
    [SerializeField] float maxBowCharge;

    [Range(0, 3)]
    [SerializeField] float minBowChargeToFire = 0.5f;

    float bowCharge;
    bool canFire = true;

    private InputManager inputManager;

    private void Start()
    {
        bowPowerSlider.value = 0f;
        bowPowerSlider.maxValue = maxBowCharge;

        inputManager = FindObjectOfType<InputManager>();
        playerMovement = GetComponent<PlayerMovement>(); // Inicializamos playerMovement
    }

    private void Update()
    {
        HandleBowPositionAndRotation();

        if (inputManager.IsActionButtonHold && canFire)
        {
            ChargeBow();
        }
        else if (inputManager.IsSelectionButtonHold && canFire && bowCharge >= minBowChargeToFire)
        {
            FireBow();
        }
        else
        {
            if (bowCharge > 0f)
            {
                bowCharge -= 1f * Time.deltaTime;
            }
            else
            {
                bowCharge = 0f;
                canFire = true;
            }

            bowPowerSlider.value = bowCharge;
        }
    }

    void HandleBowPositionAndRotation()
    {
        // Hacer que el arco mire en la dirección del movimiento del jugador
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
            yOffset = 0; // Priorizar movimiento horizontal
        }
        else
        {
            xOffset = 0; // Priorizar movimiento vertical
        }

        return new Vector3(xOffset, yOffset, 0);
    }

    void ChargeBow()
    {
        arrowGFX.enabled = true;

        bowCharge += Time.deltaTime;
        bowPowerSlider.value = bowCharge;

        if (bowCharge > maxBowCharge)
        {
            bowCharge = maxBowCharge;
            bowPowerSlider.value = maxBowCharge;
        }
    }

    void FireBow()
    {
        if (bowCharge > maxBowCharge) bowCharge = maxBowCharge;

        float arrowSpeed = bowCharge + bowPower;
        float arrowDamage = bowCharge * bowPower;

        Quaternion playerRotation = bow.rotation;
        Quaternion rot = playerRotation * Quaternion.Euler(new Vector3(0f, 0f, -90f));

        GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, rot);
        Arrow arrowScript = arrow.GetComponent<Arrow>();
        arrowScript.ArrowVelocity = arrowSpeed;
        arrowScript.ArrowDamage = arrowDamage;

        bowCharge = 0f; // Restablece la carga después de disparar
        bowPowerSlider.value = 0f;
        canFire = false;
        arrowGFX.enabled = false;
    }
}
