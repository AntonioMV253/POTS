using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum DoorType
{
    key,
    enemy,
    button
}
public class Door : Interactable
{
    [Header("Door variables")]
    public DoorType thisDoorType;
    public bool open = false;
    public SpriteRenderer doorSprite;
    public BoxCollider2D physicsCollider;

    private UIManager uiManager; // Referencia al UIManager
    private InputManager inputManager; // Referencia al InputManager

    private void Awake()
    {
        uiManager = FindObjectOfType<UIManager>(); // Buscar el UIManager en la escena
        inputManager = FindObjectOfType<InputManager>(); // Buscar el InputManager en la escena
    }

    private void Update()
    {
        if (inputManager.IsSelectionButtonHold) // Cambiar esto a tu lógica
        {
            if (playerInRange && thisDoorType == DoorType.key)
            {
                // Does the player have enough bombs?
                if (uiManager != null && uiManager.TotalBombas > 0)
                {
                    // Reduce the bomb count
                    uiManager.RestarBombas();
                    // If so, then call the open method
                    Open();
                }
            }
        }
    }

    public void Open()
    {
        // Turn off the door's sprite renderer
        doorSprite.enabled = false;
        // Set open to true
        open = true;
        // Turn off the door's box collider
        physicsCollider.enabled = false;
    }

    public void Close()
    {
        // Implement close logic if needed
    }
}

