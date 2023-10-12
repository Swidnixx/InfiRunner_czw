using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 2;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1);
        bool grounded = hit.collider != null;
        if(Input.GetMouseButtonDown(0) && grounded)
        {
            Vector2 velocity = new Vector2(0, jumpForce);
            rb.velocity = velocity;
        }

        //Raycast debug
        Color c = grounded ? Color.green : Color.red;
        Debug.DrawRay(transform.position, Vector2.down * 1, c);
    }
}
