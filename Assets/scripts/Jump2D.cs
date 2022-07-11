using UnityEngine;


public class Jump2D : MonoBehaviour
{
    [Header("Jump")] public float jumpForce;

    [Header("Ground Detection")] [Range(0, 1)]
    public float groundCheckRadius;

    public LayerMask mask;
    public Transform feet;

    [SerializeField]
    private Rigidbody2D rb2D;
    private int jumpCount = 0;
    public bool isGrounded;
    float JumpCoolDown;
    private int extraJump=1;
    private void Awake()
    {
        rb2D= GetComponent<Rigidbody2D>();
    }

    public void Jump()
    {
        
        if (isGrounded || jumpCount < extraJump)
        {
            Vector2 vel = rb2D.velocity;
            vel.y = jumpForce;
            rb2D.velocity = vel;
            jumpCount++;
            Debug.Log(2);
        }
            

    }
    private void Update()
    {
        var test = Physics2D.OverlapCircle(feet.position, groundCheckRadius, mask);
        isGrounded = test != null;
        
        if (isGrounded == true)
        {
            jumpCount = 0;
            JumpCoolDown = Time.time + 0.2f;
        }
        else if (Time.time< JumpCoolDown)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        
    }

   
}

