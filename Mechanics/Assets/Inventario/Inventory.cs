using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<InventorySlot> slots = new List<InventorySlot>();

    public bool AddItem(int itemId, int amount)
    {
        foreach (InventorySlot slot in slots)
        {
            if (slot.itemId == itemId)
            {
                // Si la ranura ya tiene el mismo tipo de elemento, agrega la cantidad.
                if (slot.amount + amount <= 16)
                {
                    slot.amount += amount;
                    return true; // Elemento agregado con éxito
                }
                else
                {
                    // Si el límite se alcanza, no agregamos más elementos
                    return false;
                }
            }
        }

        // Si no encontramos una ranura con el mismo tipo, intentamos encontrar una ranura vacía.
        foreach (InventorySlot slot in slots)
        {
            if (slot.itemId == 0)
            {
                slot.itemId = itemId;
                slot.amount = Mathf.Min(amount, 16);
                return true; // Elemento agregado con éxito
            }
        }
        return false; // Si no se encuentra espacio en el inventario
    }
}

[System.Serializable]
public class InventorySlot
{
    public int itemId; // ID del elemento (0 para ninguna ranura)
    public int amount; // Cantidad de elementos en la ranura
}
