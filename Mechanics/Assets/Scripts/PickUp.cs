using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickUp : MonoBehaviour
{
    public Transform holdSpot;
    public LayerMask pickUpMask;
    public GameObject destroyEffectPrefab;
    public Vector3 Direction { get; set; }
    private GameObject itemHolding;
    private InputManager inputManager;
    private bool isHoldingItem = false;

    private void Start()
    {
        inputManager = FindObjectOfType<InputManager>();
    }

    void Update()
    {
        if (inputManager.IsSelectionButtonHold)
        {
            if (!isHoldingItem)
            {
                Collider2D pickUpItem = Physics2D.OverlapCircle(transform.position + Direction, .4f, pickUpMask);
                if (pickUpItem)
                {
                    itemHolding = pickUpItem.gameObject;
                    itemHolding.transform.position = holdSpot.position;
                    itemHolding.transform.parent = transform;
                    if (itemHolding.GetComponent<Rigidbody2D>())
                        itemHolding.GetComponent<Rigidbody2D>().simulated = false;

                    isHoldingItem = true;
                }
            }
        }
        else if (isHoldingItem && inputManager.IsRunButtonHold)
        {
            if (itemHolding)
            {
                StartCoroutine(ThrowItem(itemHolding));
                itemHolding = null;
            }
        }

        if (inputManager.IsActionButtonHold)
        {
            if (itemHolding)
            {
                itemHolding.transform.position = transform.position + Direction;
                itemHolding.transform.parent = null;
                if (itemHolding.GetComponent<Rigidbody2D>())
                    itemHolding.GetComponent<Rigidbody2D>().simulated = true;
                itemHolding = null;
            }
        }
    }

    IEnumerator ThrowItem(GameObject item)
    {
        Vector3 startPoint = item.transform.position;
        Vector3 endPoint = transform.position + Direction * 2;
        item.transform.parent = null;

        for (int i = 0; i < 25; i++)
        {
            item.transform.position = Vector3.Lerp(startPoint, endPoint, i * 0.04f);
            yield return null;
        }

        if (item.GetComponent<Rigidbody2D>())
            item.GetComponent<Rigidbody2D>().simulated = true;

        // Instantiate the destroy effect
        GameObject destroyEffectInstance = Instantiate(destroyEffectPrefab, item.transform.position, Quaternion.identity);
        Destroy(destroyEffectInstance, 2.0f); // Destroy the effect after 2 seconds

        Destroy(item); // Destroy the item
        isHoldingItem = false;
    }

}

