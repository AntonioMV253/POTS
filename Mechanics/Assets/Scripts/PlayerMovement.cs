using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.SceneManagement;

public enum PlayerState
{
    Walk,
    Run,
    Attack,
    Stagger,
    Idle
}

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    private Animator animator;
    public Vector2 moveInput;
    public PlayerState currentState;
    private InputManager inputManager;
    private AudioSource audioSource;
    public FloatValue currentHealth;
    public SignalSender playerHealthSignal;
    public PickUp pickUpScript;
    public VectorValue startingPosition;

    public float Speed = 4;
    public float MaxSpeed = 8;
    public AudioClip[] walkSounds;
    public AudioClip runSound;
    public float minPitch = 0.8f;
    public float maxPitch = 1.2f;

    private float staggerTime = 1.0f;
    private bool isStaggered = false;

    [Header("Frames Daño")]
    public Color flashColor;
    public Color regularColor;
    public float flashDuration;
    public int numberOfFlashes;
    public Collider2D triggerCollider;
    public SpriteRenderer mySprite;

    private void Awake()
    {
        currentState = PlayerState.Walk;
        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        inputManager = GetComponent<InputManager>();
        transform.position = startingPosition.initialValue;
        audioSource = GetComponent<AudioSource>();

        audioSource.loop = false;
        audioSource.playOnAwake = false;
        audioSource.volume = 0.5f;
    }

    private void FixedUpdate()
    {
        if (isStaggered)
        {
            return;
        }

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

            if (currentState == PlayerState.Walk && !audioSource.isPlaying)
            {
                PlayRandomWalkSound();
            }
        }
        else
        {
            animator.SetBool("Moving", false);
        }
    }

    private void PlayRandomWalkSound()
    {
        AudioClip walkSound = walkSounds[Random.Range(0, walkSounds.Length)];

        audioSource.clip = walkSound;
        audioSource.pitch = Random.Range(minPitch, maxPitch);
        audioSource.Play();
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
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            audioSource.clip = runSound;
            audioSource.pitch = 1.0f;
            audioSource.Play();
        }
        else
        {
            currentState = PlayerState.Walk;
        }
    }

    public void OnAction(InputValue input)
    {
        if (isStaggered)
        {
            return;
        }

        if (input.isPressed && currentState != PlayerState.Attack && currentState != PlayerState.Stagger)
        {
            StartCoroutine(AttackCo());
        }
        else if (currentState == PlayerState.Walk || currentState == PlayerState.Idle)
        {
            UpdateAnimationAndMove();
        }
    }

    public void Knock(float knockTime, float damage)
    {
        if (isStaggered)
        {
            return;
        }

        currentHealth.RuntimeValue -= damage;
        playerHealthSignal.Raise();

        if (currentHealth.RuntimeValue <= 0)
        {
            SceneManager.LoadScene("MenuInicial");
        }
        else
        {
            StartCoroutine(KnockCo(knockTime));
        }
    }

    private IEnumerator KnockCo(float knockTime)
    {
        if (myRigidbody != null)
        {
            StartCoroutine(FlashCo());
            yield return new WaitForSeconds(knockTime);
            isStaggered = false;
            currentState = PlayerState.Walk;
        }
    }

    private IEnumerator FlashCo()
    {
        int temp = 0;
        triggerCollider.enabled = false;
        while (temp < numberOfFlashes)
        {
            mySprite.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            mySprite.color = regularColor;
            yield return new WaitForSeconds(flashDuration);
            temp++;
        }
        triggerCollider.enabled = true;
    }

    private IEnumerator AttackCo()
    {
        if (isStaggered)
        {
            yield break;
        }

        currentState = PlayerState.Attack;
        animator.SetBool("Attacking", true);

        yield return new WaitForSeconds(0.33f);

        animator.SetBool("Attacking", false);
        currentState = (inputManager.IsRunButtonHold) ? PlayerState.Run : PlayerState.Walk;
    }
}
