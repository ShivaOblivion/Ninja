using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dash : MonoBehaviour
{
    [SerializeField] public Rigidbody2D rb;
    private Vector2 dashDirection;
    public float dashSpeed;
    public Transform attackPos;

    [Header("Range d'attaque")] [Range(0, 5)]
    public float attackRange;

    private bool canDash;
    public bool isDashing;
    private float dashingTime = .2f;
    private float dashingCooldown;

    public TrailRenderer tr;

    public int damage;
    public LayerMask whatIsEnemies;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        canDash = true;
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {


            Collider2D[] enemiesToDamoge = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
            for (int i = 0; i < enemiesToDamoge.Length; i++)
            {
                enemiesToDamoge[i].GetComponent<EnemyHealth>().TakeDamage(damage);
                Debug.Log("attack!!");

            }
        }
    }

    public IEnumerator DashAtt()
    {
        canDash = false;
        isDashing = true;
        float originalgravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = dashDirection*dashSpeed;
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalgravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    public void OnStickMoved(InputAction.CallbackContext val)
    {
        dashDirection = val.ReadValue<Vector2>();
       
    }

    public void Dashin()
    {
        if (canDash)
        {
            StartCoroutine(DashAtt());
            Debug.Log("dash");
        }
        
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
