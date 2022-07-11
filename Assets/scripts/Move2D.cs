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
    }
    
    public void OnStickMoved(InputAction.CallbackContext val )
    {
        _stickDirection = val.ReadValue<float>();
        Debug.Log("Horizontal");
    }
}
