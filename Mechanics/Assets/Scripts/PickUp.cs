using System.Collections;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public Transform holdSpot;
    public LayerMask pickUpMask;
    public GameObject destroyEffectPrefab;
    private GameObject itemHolding;
    private InputManager inputManager;
    private bool isHoldingItem = false;
    public float throwForce = 10f;

    public SpriteRenderer bowSpriteRenderer;

    private bool canThrow = true; // Permite lanzar solo si se ha recogido un objeto

    private void Start()
    {
        inputManager = GetComponent<InputManager>();
        if (!bowSpriteRenderer)
        {
            Debug.LogWarning("No se ha asignado el SpriteRenderer del arco.");
        }
    }

    void Update()
    {
        if (inputManager.IsSelectionButtonHold && !isHoldingItem)
        {
            Collider2D pickUpItem = Physics2D.OverlapCircle(transform.position, 0.4f, pickUpMask);
            if (pickUpItem)
            {
                itemHolding = pickUpItem.gameObject;
                itemHolding.transform.position = holdSpot.position;
                itemHolding.transform.parent = holdSpot;
                Rigidbody2D rb = itemHolding.GetComponent<Rigidbody2D>();

                if (rb)
                    rb.simulated = false;

                isHoldingItem = true;
                canThrow = true; // El jugador puede lanzar el objeto

                if (bowSpriteRenderer)
                    bowSpriteRenderer.enabled = false;
            }
        }
        else if (isHoldingItem && canThrow && inputManager.IsRunButtonHold)
        {
            canThrow = false; // Evita que el jugador lance el objeto mientras sostiene el botón Run

            if (itemHolding)
            {
                itemHolding.transform.parent = null;
                Rigidbody2D rb = itemHolding.GetComponent<Rigidbody2D>();

                if (rb)
                {
                    rb.simulated = true;

                    // Calcula la dirección de lanzamiento basada en la rotación del jugador
                    Vector3 playerDirection = Vector3.up; // Inicialmente, asumimos que el jugador mira hacia arriba

                    // Cambia la dirección en función de la entrada del jugador
                    if (inputManager.MoveInput.x > 0)
                    {
                        playerDirection = Vector3.right;
                    }
                    else if (inputManager.MoveInput.x < 0)
                    {
                        playerDirection = Vector3.left;
                    }
                    else if (inputManager.MoveInput.y < 0)
                    {
                        playerDirection = Vector3.down;
                    }

                    Vector3 throwDirection = playerDirection.normalized * throwForce;

                    rb.velocity = throwDirection;
                }

                StartCoroutine(DestroyItem(itemHolding));
                itemHolding = null;

                if (bowSpriteRenderer)
                    bowSpriteRenderer.enabled = true;
            }
        }
    }

    IEnumerator DestroyItem(GameObject item)
    {
        yield return new WaitForSeconds(2f);

        if (destroyEffectPrefab)
        {
            GameObject destroyEffectInstance = Instantiate(destroyEffectPrefab, item.transform.position, Quaternion.identity);
            Destroy(destroyEffectInstance, 2.0f);
        }

        Destroy(item);
        isHoldingItem = false;
    }
}
