using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public Vector2 MoveInput;
    public bool IsRunButtonHold;
    public bool IsActionButtonHold;
    public bool IsSelectionButtonHold;
    public Vector2 TouchPosition;
    public bool IsTouchPressed;

    public void OnMove(InputValue input)
    {
        Vector2 newMoveInput = input.Get<Vector2>();
        if (newMoveInput != Vector2.zero)
        {
            MoveInput = newMoveInput;
        }
    }


    public void OnRun(InputValue input)
    {
        IsRunButtonHold = Convert.ToBoolean(input.Get<float>());
    }

    public void OnAction(InputValue input)
    {
        IsActionButtonHold = Convert.ToBoolean(input.Get<float>());
    }

    public void OnSelection(InputValue input)
    {
        IsSelectionButtonHold = Convert.ToBoolean(input.Get<float>());
    }

    public void OnTouch(InputValue input)
    {
        TouchPosition = input.Get<Vector2>();
        IsTouchPressed = true;
    }
}
