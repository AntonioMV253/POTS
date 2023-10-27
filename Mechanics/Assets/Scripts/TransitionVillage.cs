using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionVillage : MonoBehaviour
{
    public string sceneToLoad;
    public Vector2 PlayerPosition;
    public VectorValue playerStorage;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            playerStorage.initialValue = PlayerPosition;
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}