using System.Collections;
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
    public bool isUsingBow = false;

    public SpriteRenderer bowSpriteRenderer;

    private void Start()
    {
        inputManager = FindObjectOfType<InputManager>();
        if (!bowSpriteRenderer)
        {
            Debug.LogWarning("No se ha asignado el SpriteRenderer del arco.");
        }
    }

    void Update()
    {
        if (isUsingBow) return;

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

                    if (bowSpriteRenderer)
                        bowSpriteRenderer.enabled = false;
                }
            }
        }
        else if (isHoldingItem && inputManager.IsRunButtonHold)
        {
            if (itemHolding)
            {
                StartCoroutine(ThrowItem(itemHolding));
                itemHolding = null;

                if (bowSpriteRenderer)
                    bowSpriteRenderer.enabled = true;
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
        item.transform.parent = null;
        Rigidbody2D rb = item.GetComponent<Rigidbody2D>();
        if (rb)
        {
            rb.simulated = true;
            rb.AddForce(Direction * 200f, ForceMode2D.Impulse);
        }

        yield return new WaitForSeconds(2f);
        GameObject destroyEffectInstance = Instantiate(destroyEffectPrefab, item.transform.position, Quaternion.identity);
        Destroy(destroyEffectInstance, 2.0f);
        Destroy(item);
        isHoldingItem = false;
    }
}
