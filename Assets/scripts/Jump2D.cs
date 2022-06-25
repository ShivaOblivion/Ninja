using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Jump2D : MonoBehaviour
{
    [Header("Jump")] public float jumpForce;

    [Header("Ground Detection")] [Range(0, 1)]
    public float groundCheckRadius;

    public LayerMask mask;
    public Transform feet;
    [HideInInspector] public bool isGrounded;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnJump()
    {
        if (isGrounded)
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void Update()
    {
        var test = Physics2D.OverlapCircle(feet.position, groundCheckRadius, mask);
        isGrounded = test != null;
    }
}
