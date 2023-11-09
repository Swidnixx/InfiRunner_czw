using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public LayerMask groundMask; 
    public float jumpForce = 2;
    public float pushForce = 200;
    Rigidbody2D rb;
    BoxCollider2D boxCollider;
    bool grounded; //Player on the ground
    bool doubleJumped; // Player already jumped twice
    bool jumpHeld; // Used to know if player holds jump button

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        //Detecting Ground
        RaycastHit2D hit = Physics2D.BoxCast( boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundMask);
        grounded = hit.collider != null;

        //Jumping
        if(Input.GetMouseButtonDown(0))
        {
            if(grounded)
            {
                doubleJumped = false;
                Vector2 velocity = new Vector2(0, jumpForce);
                rb.velocity = velocity;
            }
            else if (!doubleJumped)
            {
                doubleJumped = true;
                Vector2 velocity = new Vector2(0, jumpForce);
                rb.velocity = velocity;
            }
        }

        jumpHeld = Input.GetMouseButton(0);
    }

    private void FixedUpdate()
    {
        //Falling
        if (jumpHeld && rb.velocity.y < 0)
        {
            Vector2 force = new Vector2(0, pushForce * Mathf.Abs(rb.velocity.y));
            rb.AddForce(force);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (boxCollider == null) return; //Guard clause if boxCld is null

        //Raycast debug
        Color c = grounded ? Color.green : Color.red;
        Gizmos.color = c;
        Gizmos.DrawWireCube( boxCollider.bounds.center + Vector3.down * 0.1f, boxCollider.bounds.size);
    }
}
