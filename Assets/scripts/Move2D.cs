using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Move2D : MonoBehaviour
{
    private Rigidbody2D _rb;
    public float movSpeed;
    private float _stickDirection;
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    
    void FixedUpdate()
    {
        var horizontalVelocity = _stickDirection * movSpeed;
        var verticalVelocity = _rb.velocity.y;
        _rb.velocity = new Vector2(horizontalVelocity, verticalVelocity);
        
        //filip direction
        if (_rb.velocity.x>0)
        {
            transform.localScale=Vector3.one; 
        }
        else if(_rb.velocity.x<0)
        {
            transform.localScale =  new Vector3(-1f, 1, 1f);
        }
        
        
        
    }
    public void OnStickMoved(InputAction.CallbackContext val )
    {
        _stickDirection = val.ReadValue<float>();
        Debug.Log("Horizontal");
    }
    
    
    
}
