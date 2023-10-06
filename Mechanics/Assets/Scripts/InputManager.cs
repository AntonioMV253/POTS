using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public Vector2 MoveInput;
    public bool IsRunButtonHold;
    public bool IsActionButtonHold;
    public bool IsSelectionButtonHold;
    public void OnMove(InputValue input)
    {
        MoveInput = input.Get<Vector2>();
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
}