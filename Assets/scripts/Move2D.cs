
using UnityEngine;
using UnityEngine.InputSystem;

public class Move2D : MonoBehaviour
{
    private Rigidbody2D _rb;
    public float movSpeed;
    public float stickDirection;
    public Jump2D jump2D;
    public dashAttack dashAttack;
    public Animator animator;
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    
    void FixedUpdate()
    {
        var horizontalVelocity = stickDirection * movSpeed;
        var verticalVelocity = _rb.velocity.y;
        _rb.velocity = new Vector2(horizontalVelocity, verticalVelocity);

        animator.SetFloat("Speed", Mathf.Abs(horizontalVelocity));
        
        //filip direction
        if (_rb.velocity.x>0)
        {
            transform.localScale=Vector3.one; 
        }
        else if(_rb.velocity.x<0)
        {
            transform.localScale =  new Vector3(-1f, 1, 1f);
        }
        
        if (jump2D.isGrabbing)
        { 
            _rb.velocity = Vector2.zero;
        }
        

    }
    public void OnStickMoved(InputAction.CallbackContext val )
    {
        stickDirection = val.ReadValue<float>();
        Debug.Log("Horizontal");
    }
    
    
    
}
