using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIA : MonoBehaviour
{
   public Rigidbody2D rb2d;
   public float speed;
   public bool isInvincible = false;
   private bool isHitted = false;
   [SerializeField] private float m_DashForce = 25f;
   private bool isDashing = false;
   public GameObject enemy;
   private float distToPlayerX;
   private float distToPlayerY;
   public float meleeDist = 1.5f;
   public float rangeDist = 5f;
   private bool canAttack = true;
   private Transform attackCheck;
   private Animator anim;
   public float dmgValue = 1;

   void Awake()
   {
      rb2d = GetComponent<Rigidbody2D>();
       anim= GetComponent<Animator>();
   }  
   
   
   
   
   
   
   
}
