using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Config
    public float moveSpeed = 5f;
    public float jumpSpeed = 5f;
    public float climbSpeed = 2f;
    public Vector2 deathKick = new Vector2(0f, 20f);
    public AudioClip jumpSound;
    public AudioClip deathSound;

    // State
    private bool isAlive = true;

    // Cached component references
    private Rigidbody2D rb;
    private float horizontalDirection;
    private float verticalDirection;
    private bool isFacingRight = true;
    private Animator animator;
    private BoxCollider2D myBodyCollider;
    private CapsuleCollider2D myFeetCollider;
    private float gravityScaleAtStart;

    // Message then methods

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        myBodyCollider = GetComponent<BoxCollider2D>();
        myFeetCollider = GetComponent<CapsuleCollider2D>();
        gravityScaleAtStart = rb.gravityScale;
    }

    void Update()
    {
        if (isAlive)
        {
            HandleInput();
            CheckMovementDirection();
            ClimbLadder();
            Die();
        }
    }

    private void FixedUpdate()
    {
        Run();
    }

    private void HandleInput()
    {
        horizontalDirection = Input.GetAxisRaw("Horizontal");
        verticalDirection = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void Run()
    {
        rb.velocity = new Vector2(horizontalDirection * moveSpeed, rb.velocity.y);

        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        animator.SetBool("Running", playerHasHorizontalSpeed);
    }

    private void Jump()
    {
        // Jump Once
        if (myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            AudioSource.PlayClipAtPoint(jumpSound, Camera.main.transform.position);
        }
    }

    private void Die()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazard")))
        {
            isAlive = false;
            animator.SetTrigger("Dying");
            rb.velocity = deathKick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
            AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position);
        }
    }

    private void CheckMovementDirection()
    {
        if (isFacingRight && horizontalDirection < 0)
        {
            FlipSprite();
        }
        else if (!isFacingRight && horizontalDirection > 0)
        {
            FlipSprite();
        }
    }

    private void FlipSprite()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void ClimbLadder()
    {
        if (myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            rb.velocity = new Vector2(rb.velocity.x, verticalDirection * climbSpeed);
            bool playerVerticalSpeed = Mathf.Abs(rb.velocity.y) > Mathf.Epsilon;
            animator.SetBool("Climbing", playerVerticalSpeed);
            rb.gravityScale = 0f;
        }
        else
        {
            rb.gravityScale = gravityScaleAtStart;
            animator.SetBool("Climbing", false);
        }
    }

}
