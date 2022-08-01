using System;
using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float timeBtwAttack;
    public float statTimeBtwAttack;
    public Transform attackPos;
    public bool isAttackni;
    public Animator animator;


    [Header("Range d'attaque")] [Range(0, 5)]
    public float attackRange;

    public int damage;
    public LayerMask whatIsEnemies;

    private void FixedUpdate()
    {
        if (isAttackni)
        {
            animator.SetBool("IsAttackin", true);
        }
        else
        {
            animator.SetBool("IsAttackin", false);    
        }
    }

    private void Update()
    {
        if (timeBtwAttack<=0)
        {
            
                
            timeBtwAttack = statTimeBtwAttack;
        }
        else
        {
            timeBtwAttack = statTimeBtwAttack;
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

   public void Attack()
   {
       isAttackni = true;
        Debug.Log("test");
        Collider2D[] enemiesToDamoge = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
        for (int i = 0; i < enemiesToDamoge.Length; i++)
        {
            enemiesToDamoge[i].GetComponent<EnemyHealth>().TakeDamage(damage);
            Debug.Log("attack!!");
                    
        }
        StartCoroutine(Att());
       
   }

   public IEnumerator Att()
   {
       yield return new WaitForSeconds(.2F);
       isAttackni = false;
   }
}