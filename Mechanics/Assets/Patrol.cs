using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public float speed;
    public float waitTime;
    public Transform[] moveSpots;
    private int randomSpot;
    private Vector2 moveDirection; // Variable para almacenar la dirección de movimiento
    private Animator animator; // Variable para el Animator

    void Start()
    {
        animator = GetComponent<Animator>(); // Obtener referencia al Animator
        StartCoroutine(Move());
    }

    void Update()
    {
        UpdateAnimationAndMove();
    }

    private void UpdateAnimationAndMove()
    {
        if (moveDirection.magnitude > 0)
        {
            animator.SetFloat("MoveX", moveDirection.x);
            animator.SetFloat("MoveY", moveDirection.y);
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
        }
    }

    IEnumerator Move()
    {
        while (true)
        {
            randomSpot = Random.Range(0, moveSpots.Length);

            Vector3 destination = moveSpots[randomSpot].position;

            // Calcular la dirección basada en si x es mayor que y
            if (Mathf.Abs(destination.x - transform.position.x) > Mathf.Abs(destination.y - transform.position.y))
            {
                moveDirection = new Vector2(Mathf.Sign(destination.x - transform.position.x), 0);
            }
            else
            {
                moveDirection = new Vector2(0, Mathf.Sign(destination.y - transform.position.y));
            }

            while (Vector3.Distance(transform.position, destination) > 0.2f)
            {
                transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
                yield return null;
            }

            yield return new WaitForSeconds(waitTime);
        }
    }
}
