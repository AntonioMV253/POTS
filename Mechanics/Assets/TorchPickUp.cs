using System.Collections;
using UnityEngine;

public class TorchPickUp : MonoBehaviour
{
    public LayerMask pickUpMask;
    public float torchPickUpCooldown = 0.5f; // Tiempo de enfriamiento para recoger la antorcha nuevamente

    private GameObject torchHolding;
    private InputManager inputManager;
    private bool isHoldingTorch = false;
    private Transform originalParent;
    private float lastPickUpTime;

    private void Start()
    {
        inputManager = GetComponent <InputManager>();
        lastPickUpTime = -torchPickUpCooldown; 
    }

    private void Update()
    {
        if (inputManager.IsSelectionButtonHold)
        {
            float timeSinceLastPickUp = Time.time - lastPickUpTime;

            if (!isHoldingTorch)
            {
                Collider2D pickUpTorch = Physics2D.OverlapCircle(transform.position, 0.4f, pickUpMask);
                if (pickUpTorch && timeSinceLastPickUp >= torchPickUpCooldown)
                {
                    torchHolding = pickUpTorch.gameObject;
                    originalParent = torchHolding.transform.parent; 
                    torchHolding.transform.position = transform.position;
                    torchHolding.transform.parent = transform;

                    Rigidbody2D rb = torchHolding.GetComponent<Rigidbody2D>();
                    if (rb)
                    {
                        rb.simulated = false;
                    }

                    isHoldingTorch = true;
                    lastPickUpTime = Time.time;
                }
            }
            else if (timeSinceLastPickUp >= torchPickUpCooldown)
            {
                torchHolding.transform.parent = originalParent; 
                torchHolding.transform.position = transform.position;
                isHoldingTorch = false;

                Rigidbody2D rb = torchHolding.GetComponent<Rigidbody2D>();
                if (rb)
                {
                    rb.simulated = true;
                }
                lastPickUpTime = Time.time;
            }
        }
    }
}
