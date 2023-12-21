using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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

    public AudioClip jumpAudio;
    public AudioClip landSound;

    float JumpInitialVelocity => (2 * jumpHeight) /  jumpPeakDuration;

    Animator animator;

    bool previouslyGrounded;


    private void Start()
    {
        DefineGravity();
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();

        GameManager.Instance.GameOverEvent += OnGameOver;
    }
    private void OnDestroy()
    {
        GameManager.Instance.GameOverEvent -= OnGameOver;
    }

    bool over;
    void OnGameOver()
    {
        over = true;
        animator.SetTrigger("gameOver");
    }

    private void Update()
    {
        if (over) return;

        //Detecting Ground
        RaycastHit2D hit = Physics2D.BoxCast( boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundMask);
        grounded = hit.collider != null && rb.velocity.y <= 0;
        if(grounded && !previouslyGrounded)
        {
            SoundManager.Instance.PlaySfx(landSound);
            animator.SetBool("doubleJumped", doubleJumped);
        }
        previouslyGrounded = grounded;
        animator.SetBool("grounded", grounded);

        //Jumping
        bool overUI;
#if UNITY_EDITOR
        overUI = EventSystem.current.IsPointerOverGameObject();
#else
		overUI = EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId);
#endif
        if (Input.GetMouseButtonDown(0) && !overUI)
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

    void Jump()
    {
        Vector2 velocity = new Vector2(0, JumpInitialVelocity);
        rb.velocity = velocity;
        SoundManager.Instance.PlaySfx(jumpAudio);
        animator.SetTrigger("jump");
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
        if(paralyzed)
        {
            DisableTomoes();
            CancelInvoke(nameof(DisableTomoes));
        }
        tomoes.SetActive(true);
        Invoke(nameof(DisableTomoes), 3);
        paralyzed = true;
    }
    void DisableTomoes()
    {
        paralyzed = false;
        tomoes.SetActive(false);
    }
}
