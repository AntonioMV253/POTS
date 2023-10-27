using System.Collections;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] GameObject coinPrefab;
    [SerializeField] GameObject bombPrefab;
    [SerializeField] GameObject heartPrefab;
    [SerializeField] float openTime = 2f;
    [SerializeField] float splitSpeed;
    [SerializeField] bool spawn;

    private int maxDrops = 5; // Cantidad máxima de objetos a dropear
    private int currentDrops = 0; // Objetos dropeados actualmente

    // Start is called before the first frame update
    void Start()
    {
        if (!spawn) return;
        StartCoroutine(OpenCo());
        InvokeRepeating("DropRandomItem", openTime + 0.5f, splitSpeed);
    }

    IEnumerator OpenCo()
    {
        yield return new WaitForSeconds(openTime);
        anim.SetTrigger("open");
    }

    private void DropRandomItem()
    {
        if (currentDrops >= maxDrops)
        {
            CancelInvoke("DropRandomItem"); // Cancela el dropeo de objetos si hemos alcanzado el límite
            return;
        }

        float randomValue = Random.value; // Devuelve un valor entre 0 y 1
        GameObject objectToDrop;

        if (randomValue <= 0.4f) // 40% de posibilidad para monedas
        {
            objectToDrop = coinPrefab;
        }
        else if (randomValue <= 0.8f) // 40% de posibilidad para corazones
        {
            objectToDrop = heartPrefab;
        }
        else // 20% de posibilidad para bombas
        {
            objectToDrop = bombPrefab;
        }

        Instantiate(objectToDrop, transform.position, Quaternion.identity, transform);
        currentDrops++;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !spawn)
        {
            spawn = true;
            StartCoroutine(OpenCo());
            InvokeRepeating("DropRandomItem", openTime + 0.5f, splitSpeed);
        }
    }
}
