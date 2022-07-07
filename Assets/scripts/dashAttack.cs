using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class dashAttack : MonoBehaviour
{
    [SerializeField]
    public Rigidbody2D rb;
    private Vector3 dashDirection;
    private float currentDashSpeed;
    public float dashSpeed;
    private Vector3 mousePosition;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Dash()
    {
        mousePosition=
        rb.velocity =;
    }
}
