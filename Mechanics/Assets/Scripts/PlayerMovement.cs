using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public enum PlayerState
{
    Walk,
    Run,
    Attack
}

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    private Animator animator;
    private Vector2 moveInput;
    private PlayerState currentState;
    private InputManager inputManager; 

    public float Speed = 4;
    public float MaxSpeed = 8;

    private void Awake()
    {
        currentState = PlayerState.Walk;
        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        inputManager = GetComponent<InputManager>(); // Asigna la referencia al InputManager
    }

    private void FixedUpdate()
    {
        float currentSpeed = (currentState == PlayerState.Run) ? MaxSpeed : Speed;

        Vector2 moveDirection = moveInput.normalized;
        Vector2 velocity = moveDirection * currentSpeed;
        myRigidbody.velocity = velocity;

        if (currentState != PlayerState.Attack)
        {
            UpdateAnimationAndMove();
        }
    }

    private void UpdateAnimationAndMove()
    {
        if (moveInput.magnitude > 0)
        {
            animator.SetFloat("MoveX", moveInput.x);
            animator.SetFloat("MoveY", moveInput.y);
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
        }
    }

    public void OnMove(InputValue input)
    {
        moveInput = input.Get<Vector2>();
    }

    public void OnRun(InputValue input)
    {
        if (input.isPressed)
        {
            currentState = PlayerState.Run;
        }
        else
        {
            currentState = PlayerState.Walk;
        }
    }

    public void OnAction(InputValue input)
    {
        if (input.isPressed && currentState != PlayerState.Attack)
        {
            StartCoroutine(AttackCo());
        }
    }

    private IEnumerator AttackCo()
    {
        currentState = PlayerState.Attack;
        animator.SetBool("Attacking", true);

        yield return new WaitForSeconds(0.33f);

        animator.SetBool("Attacking", false);
        currentState = (inputManager.IsRunButtonHold) ? PlayerState.Run : PlayerState.Walk;
    }
}
