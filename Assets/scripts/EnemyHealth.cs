using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health;
    public GameObject enemy;
    public void Awake()
    {
        enemy = GetComponent<GameObject>();
    }

    private void Update()
    {
        if (health <= 0)
    
        {
            gameObject.SetActive(false);
            Debug.Log("JeSuisMort");
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Aïe Aïe!");
    }
}
