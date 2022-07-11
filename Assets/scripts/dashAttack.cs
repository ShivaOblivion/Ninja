using UnityEngine;
using UnityEngine.InputSystem;


public class dashAttack : MonoBehaviour
{
    [SerializeField]
    public Rigidbody2D rb;
    private Vector3 dashDirection;
    public float dashSpeed;
    private Vector3 mousePosition;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Dash()
    {
        rb.velocity = mousePosition * dashSpeed;
    }

    void OnDash()
    {
        Dash();
        Debug.Log("Dash");
    }

    public void OnmousePosition(InputAction.CallbackContext pos)
    {
        mousePosition = (pos.ReadValue<Vector2>());
    }
}

