using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    private float timeBtwAttack;
    public float statTimeBtwAttack;
    public Transform attackPos;
    
    [Header("Range d'attaque")] [Range(0, 5)]
    public float attackRange;

    public int damage;
    public LayerMask whatIsEnemies;
    
    private void Update()
    {
        if (timeBtwAttack <= 0)
        {
            Collider2D[] enemiesToDamoge = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
            for (int i = 0; i < enemiesToDamoge.Length; i++)
            {
                enemiesToDamoge[i].GetComponent<EnemyHealth>().TakeDamage(damage);
            }
            timeBtwAttack = statTimeBtwAttack;
            
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color=Color.red;
        Gizmos.DrawWireSphere(attackPos.position,attackRange);
    }
}
