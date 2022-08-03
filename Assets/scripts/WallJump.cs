using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJump : MonoBehaviour
{
    public bool canGrab, isGrabbing;
    private float _gravityStore;
    public float wallJumpTime = .2f;
    private float _wallJumpCounter;
    public float wallJumpForceX;
    public float wallJumpForceY;
    public bool iswallJumping ;
    [SerializeField] private Rigidbody2D rb2D;
    public Move2D mouv2D;
    public Transform feet;
    public LayerMask maskWall;
    [Range(0, 5)] public float wallCheckRadius;
    public Jump2D jump2D;
    

    private void Awake()
    {
        _gravityStore = rb2D.gravityScale;
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_wallJumpCounter <= 0)
        {


            canGrab = Physics2D.OverlapCircle(feet.position, wallCheckRadius, maskWall);
            isGrabbing = false;
            if (canGrab && !jump2D.isGrounded)
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
        yield return new WaitForSeconds(.3f);
        iswallJumping = false;
    }
    
}
