using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Move2D : MonoBehaviour
{
    public Rigidbody2D rb;
    public float Movspeed;
    public float HorizontalMov;
   
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void FixedUpdate()
    {
        var horizontalVelocity = HorizontalMov * Movspeed;
    }
    public void OnHorizontal(InputValue val)
    {
        HorizontalMov = val.Get<float>();
        Debug.Log("Horizontal");
    }
}
