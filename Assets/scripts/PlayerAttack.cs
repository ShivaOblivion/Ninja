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
        Debug.Log("test");
        Collider2D[] enemiesToDamoge = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
        for (int i = 0; i < enemiesToDamoge.Length; i++)
        {
            enemiesToDamoge[i].GetComponent<EnemyHealth>().TakeDamage(damage);
            Debug.Log("attack!!");
                    
        }
    }
}