using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Require components
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(TeamID))]

public class Entity : MonoBehaviour
{
    #region Variables

    // Entity attributes
    [HideInInspector]
    public TeamID.ID teamID;
    [SerializeField] protected float movementSpeed = 7;
    [SerializeField] protected float jumpForce = 6;
    [Header("Number of jumps available after touching the ground (2 for double jump)")]
    [SerializeField] protected int startingJumpCount = 2;
    private int _currentJumpCount; // This is to keep track of number of jumps used
    [SerializeField] protected float dashForce = 30;
    [SerializeField] protected float dashDuration = 0.2f;
    [SerializeField] protected int startingHealth = 100;
    private int _currentHealth;
    [Header("Invulnerability duration after taking damage (in seconds)")]
    [SerializeField] protected float invulnerabilityDuration = 2;
    [SerializeField] protected bool hasInvulnerability = false;
    [HideInInspector] public bool isInvulnerable;


    // Input
    protected float horizontalInput;
    protected bool jumpInput;
    protected bool dashInput;

    // References
    protected Rigidbody2D rigidBody2D;
    protected BoxCollider2D boxCollider2D;
    [SerializeField] protected LayerMask groundLayerMask;
    protected SpriteRenderer spriteRenderer;

    // Checks
    private bool _doGroundCheck;
    private bool _canDash;
    private bool _facingRight;

    #endregion

    #region Start

    protected virtual void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        _currentJumpCount = startingJumpCount;
        _currentHealth = startingHealth;
        isInvulnerable = false;
        _canDash = true;
    }

    #endregion

    #region Update

    protected virtual void Update()
    {
        TakeInput();

        // Check which side the character is facing and makes the character face that direction
        if (horizontalInput > 0)
        {
            _facingRight = true;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (horizontalInput < 0)
        {
            _facingRight = false;
            transform.localScale = new Vector3(-1, 1, 1);
        }

        Move();
        GroundCheck();
    }

    #endregion

    #region FixedUpdate

    protected virtual void FixedUpdate()
    {
        // Enable ground check (we do ground check like this so double jump will work properly without giving extra or less jumps)
        if (rigidBody2D.velocity.y < 0)
        {
            _doGroundCheck = true;
        }
    }

    #endregion

    #region TakeInput

    protected virtual void TakeInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        jumpInput = Input.GetButtonDown("Jump");
        dashInput = Input.GetKeyDown(KeyCode.LeftShift);
    }

    #endregion

    #region Move

    private void Move()
    {
        if (_canDash)
        {
            rigidBody2D.velocity = new Vector2(horizontalInput * movementSpeed, rigidBody2D.velocity.y);
        }

        Jump();

        // Dash
        if (dashInput && _canDash)
        {
            _canDash = false;
            isInvulnerable = true;
            // Dash towards the direction the player is facing
            if (_facingRight)
            {
                rigidBody2D.velocity = new Vector2(dashForce, rigidBody2D.velocity.y);
            }
            else
            {
                rigidBody2D.velocity = new Vector2(-dashForce, rigidBody2D.velocity.y);
            }
            StartCoroutine(Dash());
        }
    }

    #endregion

    #region Dash

    // This is not the actual dash but is a delay and resets character speed and colour to the normal colour after the dash
    IEnumerator Dash()
    {
        yield return new WaitForSeconds(dashDuration);
        rigidBody2D.velocity = new Vector2(0, rigidBody2D.velocity.y);
        _canDash = true;
        isInvulnerable = false;
    }

    #endregion

    #region Jump

    private void Jump()
    {
        // Jump
        if (_currentJumpCount > 0 && jumpInput)
        {
            _currentJumpCount--;
            rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, 0);
            rigidBody2D.AddForce(new Vector2(0f, jumpForce * 100));
        }
    }

    #endregion

    #region Ground Check
    private void GroundCheck()
    {
        float extraExtents = 0.1f; // Default was 0.05f
        RaycastHit2D rayCastHit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, extraExtents, groundLayerMask);

        // Ground check
        if (_doGroundCheck)
        {
            if (rayCastHit.collider)
            {
                _doGroundCheck = false;
                _currentJumpCount = startingJumpCount;
            }
        }
    }
    #endregion

    #region Collisions

    // Doing collisions like this makes sure collision works with obstacles with or without rigidbodies

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnCollision(collision.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnCollision(collision.gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        OnCollision(collision.gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        OnCollision(collision.gameObject);
    }

    private void OnCollision(GameObject collider)
    {

    }

    #endregion

    #region TakeDamage

    public void TakeDamage(int damageValue)
    {
        if (!isInvulnerable)
        {
            _currentHealth -= damageValue;
            if (hasInvulnerability)
            {
                isInvulnerable = true;
                StartCoroutine(Invulnerability(invulnerabilityDuration));
            }
        }
    }

    #endregion

    #region Invulnerability

    private IEnumerator Invulnerability(float duration)
    {
        yield return new WaitForSeconds(duration); // Delay here

        // Do stuff after delay
        isInvulnerable = false;
    }

    #endregion
}
