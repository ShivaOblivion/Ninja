using UnityEngine;
using System.Collections;


public class Jump2D : MonoBehaviour
{
    [Header("Jump")] public float jumpForce;

    [Header("Detection")] [Range(0, 5)] public float groundCheckRadius;
    

    public LayerMask maskGround;
    
    public Transform feet;

    [SerializeField] private Rigidbody2D rb2D;
    private int _jumpCount;
    public bool isGrounded;
    float _jumpCoolDown;
    private int extraJump = 2;
    public Dash dashAttack;
    public Move2D mouv2D;
    public float jetJumpForce;
    public WallJump wallJump;
    public bool isJumping;
    
    
    
    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
       
    }

    public void Jump()
    {
        if (dashAttack.isDashing)
        {
            return;
            
        }
        if (!wallJump.isGrabbing && !wallJump.iswallJumping)
        {
            if (isGrounded && _jumpCount < extraJump)
            {
                Vector2 vel = rb2D.velocity;
                vel.y = jumpForce;
                vel.x = mouv2D.stickDirection * mouv2D.movSpeed;
                rb2D.velocity = vel;
                _jumpCount++;
                Debug.Log("Jump");
                StartCoroutine(Jumping());
            }
            else if (_jumpCount < extraJump)
            {
                Vector2 vel = rb2D.velocity;
                vel.y = jetJumpForce;
                rb2D.velocity = vel;
                Debug.Log("jetJump");
                _jumpCount++;
                StartCoroutine(Jumping());
            }
            else
            {
                Debug.Log("0");
            }
        }
    }

    private void Update()
    {
        if (isGrounded)
        {
            
        }
        else
        {
            
        }

        if (isJumping)
        {
            
        }
        else
        {
            
        }
        
        
        
        var test = Physics2D.OverlapCircle(feet.position, groundCheckRadius, maskGround);
        isGrounded = test != null;

        if (isGrounded || wallJump.isGrabbing)
        {
            _jumpCount = 0;
            _jumpCoolDown = Time.time + .2f;
        }
        else if (Time.time < _jumpCoolDown)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        

    }
    public IEnumerator Jumping()
    {
        isJumping = true;
        yield return new WaitForSeconds(.2f);
        isJumping = false;
    }
}


