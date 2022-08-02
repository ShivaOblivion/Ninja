using UnityEngine;
using System.Collections;


public class Jump2D : MonoBehaviour
{
    [Header("Jump")] public float jumpForce;

    [Header("Detection")] [Range(0, 5)] public float groundCheckRadius;
    [Range(0, 5)] public float wallCheckRadius;

    public LayerMask maskGround;
    public LayerMask maskWall;
    public Transform feet;

    [SerializeField] private Rigidbody2D rb2D;
    private int _jumpCount;
    public bool isGrounded;
    float _jumpCoolDown;
    private int extraJump = 2;
    public Dash dashAttack;
    public bool canGrab, isGrabbing;
    public Move2D mouv2D;
    private float _gravityStore;
    public float wallJumpTime = .2f;
    private float _wallJumpCounter;
    public float jetJumpForce;
    public float wallJumpForceX;
    public float wallJumpForceY;
    public bool iswallJumping;
    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        _gravityStore = rb2D.gravityScale;
    }

    public void Jump()
    {
        if (dashAttack.isDashing)
        {
            return;
            
        }
        if (!isGrabbing && !iswallJumping)
        {
            if (isGrounded && _jumpCount < extraJump)
            {
                Vector2 vel = rb2D.velocity;
                vel.y = jumpForce;
                vel.x = mouv2D.stickDirection * mouv2D.movSpeed;
                rb2D.velocity = vel;
                _jumpCount++;
                Debug.Log("Jump");
            }
            else if (_jumpCount < extraJump)
            {
                Vector2 vel = rb2D.velocity;
                vel.y = jetJumpForce;
                rb2D.velocity = vel;
                Debug.Log("jetJump");
                _jumpCount++;
            }
            else
            {
                Debug.Log("0");
            }
        }
    }

    private void Update()
    {
        var test = Physics2D.OverlapCircle(feet.position, groundCheckRadius, maskGround);
        isGrounded = test != null;

        if (isGrounded || isGrabbing)
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

        //wall jump
        if (_wallJumpCounter <= 0)
        {


            canGrab = Physics2D.OverlapCircle(feet.position, wallCheckRadius, maskWall);
            isGrabbing = false;
            if (canGrab && !isGrounded)
            {
                if ((transform.localScale.x == 1f && mouv2D.stickDirection > 0) ||
                    (transform.localScale.x == -1f && mouv2D.stickDirection < 0))
                {
                    isGrabbing = true;
                }

            }

            if (isGrabbing)
            {
                rb2D.gravityScale = 0f;
                rb2D.velocity = Vector2.zero;
            }
            else
            {
                rb2D.gravityScale = _gravityStore;
            }
        }
        else
        {
            _wallJumpCounter -= Time.deltaTime;
        }
    }

    public void wallJump()
    {
        if (isGrabbing && _wallJumpCounter <= 0)
        {
            StartCoroutine(wallJumping());
            _wallJumpCounter = wallJumpTime;
            rb2D.velocity = new Vector2(-mouv2D.stickDirection*wallJumpForceX, wallJumpForceY);
            rb2D.gravityScale = _gravityStore;
            isGrabbing = false;
            Debug.Log("wallJump");
            


        }
        
    }

    public IEnumerator wallJumping()
    {
        iswallJumping = true;
        yield return new WaitForSeconds(.2f);
        iswallJumping = false;
    }

}


