using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{

    private Inventory inventory;
    public GameObject itemButton;
    public GameObject effect;
    public int itemId; // Agregar un ID único para cada tipo de elemento

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Intentar agregar elementos al inventario
            bool added = inventory.AddItem(itemId, 1);

            if (added)
            {
                Instantiate(effect, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}
