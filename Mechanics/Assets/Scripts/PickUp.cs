using System.Collections;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public Transform holdSpot;
    public LayerMask pickUpMask;
    public GameObject destroyEffectPrefab;

    // Referencias a los prefabs
    public GameObject heartPrefab;
    public GameObject coinPrefab;
    public GameObject bombPrefab;

    private GameObject itemHolding;
    private InputManager inputManager;
    private bool isHoldingItem = false;
    public float throwForce = 10f;

    public SpriteRenderer bowSpriteRenderer;

    private bool canThrow = true; // Permite lanzar solo si se ha recogido un objeto
    private Vector3 lastMoveInputDirection; // Almacena la última dirección de movimiento

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
        // Actualiza la dirección de movimiento constantemente
        if (inputManager.MoveInput != Vector2.zero)
        {
            lastMoveInputDirection = new Vector3(inputManager.MoveInput.x, inputManager.MoveInput.y).normalized;
        }

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
                canThrow = true;

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

                    // Calcula la dirección de lanzamiento basada en la última dirección de movimiento
                    Vector3 throwDirection = lastMoveInputDirection * throwForce;
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

        // Lógica para dropear uno de los tres objetos
        DropRandomItem(item.transform.position);

        Destroy(item);
        isHoldingItem = false;
    }

    // Función que decide qué objeto "dropear" basado en porcentajes
    void DropRandomItem(Vector3 position)
    {
        float randomValue = Random.value; // Devuelve un valor entre 0 y 1

        if (randomValue <= 0.4f) // 40% de posibilidad
        {
            Instantiate(heartPrefab, position, Quaternion.identity);
        }
        else if (randomValue <= 0.8f) // 40% de posibilidad
        {
            Instantiate(coinPrefab, position, Quaternion.identity);
        }
        else // 20% de posibilidad
        {
            Instantiate(bombPrefab, position, Quaternion.identity);
        }
    }
}
