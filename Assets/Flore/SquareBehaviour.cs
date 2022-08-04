using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class SquareBehaviour : MonoBehaviour
{
    public float dashSpeed;
    public float dashCooldown;

    private Animator animator;
    private Rigidbody2D rb;
    private Rigidbody2D player;
    private float lastDashTime;
    private float originalGravity;

    private Vector2 dashDirection => (player.position - rb.position).normalized;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        var playerInput = FindObjectOfType<PlayerInput>();
        if (playerInput == null)
            Debug.LogError("No player found!");
        player = playerInput.GetComponent<Rigidbody2D>();
        originalGravity = rb.gravityScale;
    }

    //public void Dash(Vector2 direction)
    public void Dash()
    {
        if(Time.time > lastDashTime + dashCooldown)
            DashAtt();
    }

    private void DashAtt()
    {
        Debug.Log("Je dash");
        lastDashTime = Time.time;
        rb.gravityScale = 0.1f;
        rb.velocity = dashDirection * dashSpeed;
    }

    public void StopDash()
    {
        rb.gravityScale = originalGravity;
    }
}
