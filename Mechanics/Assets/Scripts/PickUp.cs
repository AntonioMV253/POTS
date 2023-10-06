using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickUp : MonoBehaviour
{
    public Transform holdSpot;
    public LayerMask pickUpMask;
    public Sprite spriteRoto;
    public Vector3 Direction { get; set; }
    private GameObject itemHolding;
    private SpriteRenderer spriteRenderer;
    private InputManager inputManager;
    private bool isHoldingItem = false;

    private void Start()
    {
        inputManager = FindObjectOfType<InputManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
            }
        }
    }

    IEnumerator ThrowItem(GameObject item)
    {
        Vector3 startPoint = item.transform.position;
        Vector3 endPoint = transform.position + Direction * 10;
        item.transform.parent = null;

        for (float t = 0; t < 1; t += Time.deltaTime * 2)
        {
            item.transform.position = Vector3.Lerp(startPoint, endPoint, t);
            yield return null;
        }

        if (item && item.GetComponent<Rigidbody2D>())
            item.GetComponent<Rigidbody2D>().simulated = true;

        if (spriteRenderer && spriteRoto)
            spriteRenderer.sprite = spriteRoto;

        yield return new WaitForSeconds(0.1f);

        if (item)
            Destroy(item);

        isHoldingItem = false;
    }
}
