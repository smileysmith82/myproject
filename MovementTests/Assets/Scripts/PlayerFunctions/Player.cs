
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private CharacterFlip characterFlip;
    private AnimatorTest animatorTest;
    private AudioSource audioSource;
    
    [Header("Movement info")]
    [SerializeField] private float speed = 5;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private Vector2 wallJumpDirection = new Vector2(8f,16f);
    [SerializeField] private float wallJumpCooldown = 0.5f;
    [SerializeField] private float movementLockDuration = 5.0f;
    //public float fallMultiplier = 1.5f;
    private bool canDoubleJump;
    private bool canWallSlide;
    private bool canWallJump = true;
    public bool isWallSliding;
    private float movingInput;
    public bool canMove = true;
    private float lastWallJumpTime;
    private bool temporarilyDisableWallDetection;
    
    [Header("Collision info")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask whatIsGround;
                     public bool isGrounded;
    public Transform trapDetection;
    public float trapDetectionRadius = 1f;
    [SerializeField] private LayerMask oneWayPlatformLayer;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
                     private bool isWallDetected;
    private float movementLockEndTime;
    
    [Header("Audio")]
    public AudioClip jumpSound;
    [Range(0f, 1f)]
    public float jumpVolume = 0.5f;
    
    void Start()
    {
        animatorTest = GetComponent<AnimatorTest>();
        rb = GetComponent<Rigidbody2D>();
        characterFlip = GetComponent<CharacterFlip>();
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        HandleInput();
        CheckCollisions();
        animatorTest.UpdateAnimations();
        if (Time.time >= movementLockEndTime)
        {
            canMove = true;
        }
    }
    private void FixedUpdate()
    {
        HandleMovement();
    }
    private void HandleInput()
    {
        movingInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HandleJump();
        }
    }
    private void HandleJump()
    {
        if (isWallSliding && canWallJump && Time.time > lastWallJumpTime + wallJumpCooldown && !isGrounded)
        {
            WallJump();
        }
        else if (isGrounded || (isCollidingWithOneWayPlatform() && rb.velocity.y <= 0))
        {
            Jump();
        }
        else if (canDoubleJump)
        {
            canDoubleJump = false;
            Jump();
            animatorTest.TriggerDoubleJump();
        }
    }
    private bool isCollidingWithOneWayPlatform()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, oneWayPlatformLayer);
    }
    private void HandleMovement()
    {
        if (isGrounded)
        {
            canDoubleJump = true;
            canWallJump = true;
        }

        if (canMove)
        {
            if (isWallDetected && !temporarilyDisableWallDetection && movingInput!= 0 && !isGrounded)
            {
                StartWallSlide();
            }
            else
            {
                StopWallSlide();
                MoveHorizontally();
            }    
        }
        
    }
    private void MoveHorizontally()
    {
        if (canMove)
        {
            rb.velocity = new Vector2(movingInput * speed, rb.velocity.y);
        }
    }
    private void Jump()
    { 
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        //play Jump sound
        if (jumpSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(jumpSound, jumpVolume);
        }
    }
    private void WallJump()
    {
        lastWallJumpTime = Time.time;
        canMove = false; 
        movementLockEndTime = Time.time + movementLockDuration;

        rb.velocity = new Vector2(wallJumpDirection.x * -characterFlip.facingDirection/2, jumpForce);
        
        canWallJump = true;
        temporarilyDisableWallDetection = true;
        isWallSliding = false;
        Invoke(nameof(EnableWallDetection), wallJumpCooldown);
    }
    private void EnableWallDetection()
    {
        temporarilyDisableWallDetection = false;
    }
    private void StartWallSlide()
    {
        isWallSliding = true;
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * .75f);
    }
    private void StopWallSlide()
    {
        isWallSliding = false;
        
    }
    private void CheckCollisions()
    {
        CheckGroundCollision();
        CheckWallCollision();
    }
    private void CheckGroundCollision()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        if (!isGrounded && rb.velocity.y <= 0)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, oneWayPlatformLayer);
        }
    }
    private void CheckWallCollision()
    {
        if (!temporarilyDisableWallDetection)
        {
            isWallDetected = Physics2D.Raycast(wallCheck.position, Vector2.left, wallCheckDistance, whatIsGround) ||
                             Physics2D.Raycast(wallCheck.position, Vector2.right, wallCheckDistance, whatIsGround);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(trapDetection.position, trapDetectionRadius);
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance,
            wallCheck.position.y, 
            wallCheck.position.z));
    }
}