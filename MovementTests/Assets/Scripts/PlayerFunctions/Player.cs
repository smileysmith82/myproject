using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private CharacterFlip characterFlip;
    private AnimatorTest animatorTest;
    
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
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
                     private bool isWallDetected;
    
    private float movementLockEndTime;
    
    void Start()
    {
        animatorTest = GetComponent<AnimatorTest>();
        rb = GetComponent<Rigidbody2D>();
        characterFlip = GetComponent<CharacterFlip>();
    }
    // Update is called once per frame
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
        else if (isGrounded)
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
    private void HandleMovement()
    {
        if (isGrounded)
        {
            canDoubleJump = true;
            canWallJump = true;
        }

        if (canMove)
        {
            if (isWallDetected && !temporarilyDisableWallDetection && movingInput!= 0)
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
        /*if (rb.velocity.y < 0 && canWallSlide == false)
        {
            rb.velocity  += Vector2.up * Physics2D.gravity.y* (fallMultiplier) * Time.deltaTime;
        }*/
    }
    private void WallJump()
    {
        Debug.Log("Wall Jump Triggered");
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
        canMove = true;
    }
    private void CheckCollisions()
    {
        CheckGroundCollision();
        CheckWallCollision();
    }
    private void CheckGroundCollision()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
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
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance,
            wallCheck.position.y, 
            wallCheck.position.z));
    }
}