using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public Vector2 MoveInput;
    public bool IsRunButtonHold;
    public bool IsActionButtonHold;
    public bool IsSelectionButtonHold;
    public bool IsStableButtonHold;
    public Vector2 TouchPosition;
    public bool IsTouchPressed;

    public void OnMove(InputValue input)
    {
        Vector2 newMoveInput = input.Get<Vector2>();
        if (newMoveInput != Vector2.zero)
        {
            float newX = Mathf.Abs(newMoveInput.x) > Mathf.Abs(newMoveInput.y) ? Mathf.Sign(newMoveInput.x) : 0;
            float newY = Mathf.Abs(newMoveInput.x) > Mathf.Abs(newMoveInput.y) ? 0 : Mathf.Sign(newMoveInput.y);
            MoveInput = new Vector2(newX, newY);
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

    public void OnStable(InputValue input)
    {
        IsStableButtonHold = Convert.ToBoolean(input.Get<float>());
    }

    public void OnTouch(InputValue input)
    {
        TouchPosition = input.Get<Vector2>();
        IsTouchPressed = true;
    }
}
