using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    idle,
    walk,
    attack,
    stagger 
}

public class Enemy2 : MonoBehaviour
{
    public EnemyState currentState;
    public FloatValue maxHealth;
    public float health;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;
    // Start is called before the first frame update
    private void Awake()
    {
        health = maxHealth.initialValue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
