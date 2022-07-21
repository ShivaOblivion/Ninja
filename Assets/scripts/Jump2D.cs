using UnityEngine;


public class Jump2D : MonoBehaviour
{
    [Header("Jump")] public float jumpForce;

    [Header("Detection")] [Range(0, 1)]
    public float groundCheckRadius;
    [Range(0, 1)]public float wallCheckRadius;
    
    public LayerMask mask;
    public Transform feet;

    [SerializeField]
    private Rigidbody2D rb2D;
    private int _jumpCount ;
    public bool isGrounded;
    float _jumpCoolDown;
    private int extraJump=1;
    public bool canGrab, isGrabbing;
    public Move2D mouv2D;
    private void Awake()
    {
        rb2D= GetComponent<Rigidbody2D>();
    }

    public void Jump()
    {
        
        if (isGrounded || _jumpCount < extraJump)
        {
            Vector2 vel = rb2D.velocity;
            vel.y = jumpForce;
            rb2D.velocity = vel;
            _jumpCount++;
            Debug.Log(2);
            
        }
            

    }
    private void Update()
    {
        var test = Physics2D.OverlapCircle(feet.position, groundCheckRadius, mask);
        isGrounded = test != null;
        
        if (isGrounded )
        {
            _jumpCount = 0;
            _jumpCoolDown = Time.time + 0.2f;
        }
        else if (Time.time< _jumpCoolDown)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        //wall jump
        canGrab = Physics2D.OverlapCircle(feet.position, wallCheckRadius, mask);
        isGrabbing = false;
        if (canGrab && !isGrounded)
        {
            if ((transform.localScale.x== 1f &&  mouv2D.stickDirection>0)||(transform.localScale.x==-1f &&  mouv2D.stickDirection<0))
            {
                isGrabbing = true;
            }
            
        }
    }

   
}

