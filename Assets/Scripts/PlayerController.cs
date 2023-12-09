using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public LayerMask groundMask; 
    public float jumpHeight = 1.5f;
    public float jumpPeakDuration = 1.1f;
    public float pushForce = 200;
    Rigidbody2D rb;
    BoxCollider2D boxCollider;
    bool grounded; //Player on the ground
    bool doubleJumped; // Player already jumped twice
    bool jumpHeld; // Used to know if player holds jump button

    public GameObject tomoes;
    float JumpInitialVelocity => (2 * jumpHeight) /  jumpPeakDuration;

    private void Start()
    {
        DefineGravity();
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        //Detecting Ground
        RaycastHit2D hit = Physics2D.BoxCast( boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundMask);
        grounded = hit.collider != null && rb.velocity.y <= 0;

        //Jumping
        if(Input.GetMouseButtonDown(0))
        {
            if(grounded)
            {
                doubleJumped = false;
                Jump();
            }
            else if (!doubleJumped && !paralyzed)
            {
                doubleJumped = true;
                Jump();
            }
        }

        jumpHeld = Input.GetMouseButton(0);
    }

    public AudioClip jumpAudio;
    void Jump()
    {
        Vector2 velocity = new Vector2(0, JumpInitialVelocity);
        rb.velocity = velocity;
        SoundManager.Instance.PlaySfx(jumpAudio);
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
    private void OnValidate()
    {
        DefineGravity();
    }
    void DefineGravity()
    {
        float g = (-2 * jumpHeight) / Mathf.Pow(jumpPeakDuration, 2);
        Vector2 gravity = new Vector2( 0, g);
        Physics2D.gravity = gravity;
    }

    //Move to Player Effects Controller
    bool paralyzed;
    public void Kors()
    {
        tomoes.SetActive(true);
        Invoke(nameof(DisableTomoes), 5);
        paralyzed = true;
    }
    void DisableTomoes()
    {
        paralyzed = false;
        tomoes.SetActive(false);
    }
}
