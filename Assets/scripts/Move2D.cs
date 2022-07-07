using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Move2D : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;
    public float movSpeed;
    private float horizontalMov;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void FixedUpdate()
    {
        var horizontalVelocity = horizontalMov * movSpeed;
        var verticalVelocity = rb.velocity.y;
        rb.velocity = new Vector2(horizontalVelocity, verticalVelocity);
    }
    public void OnMove(InputValue val)
    {
        horizontalMov = val.Get<float>();
        Debug.Log("Horizontal");
    }
}
