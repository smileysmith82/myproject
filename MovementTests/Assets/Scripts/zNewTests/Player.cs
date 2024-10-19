using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private CharacterFlip characterFlip;
    
    [Header("Movement info")]
    [SerializeField] private float speed = 5;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private Vector2 wallJumpDirection = new Vector2(8f,16f);
    [SerializeField] private float wallJumpCooldown = 0.5f;
    
    private bool canDoubleJump;
    private bool canWallSlide;
    private bool canWallJump = true;
    private bool isWallSliding;
    private float movingInput;
    public bool canMove = true;
    private float lastWallJumpTime;
    
    private bool temporarilyDisableWallDetection;
    
    [Header("Collision info")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask whatIsGround;
                     private bool isGrounded;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
                     private bool isWallDetected;
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        characterFlip = GetComponent<CharacterFlip>();
    }
    // Update is called once per frame
    void Update()
    {
        HandleInput();
        CheckCollisions();
        HandleAnimations();
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
        if (isWallSliding && canWallJump && Time.time > lastWallJumpTime + wallJumpCooldown)
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
            TriggerDoubleJump();
        }
    }
    private void HandleMovement()
    {
        if (isGrounded)
        {
            canDoubleJump = true;
            canWallJump = true;
        }
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
    }

    private void WallJump()
    {
        Debug.Log("Wall Jump Triggered");
        lastWallJumpTime = Time.time;
        canMove = false; 
        float adjustedJumpDirection = (characterFlip.facingDirection*-5); 
        rb.velocity = new Vector2(rb.velocity.x *adjustedJumpDirection , rb.velocity.y*jumpForce);

        //Vector2 normalizedWallJumpDirection = new Vector2(characterFlip.facingDirection * wallJumpDirection.x,
            //wallJumpDirection.y).normalized;
        //float adjustedJumpForce = jumpForce * 1.5f;
        //rb.velocity = new Vector2(normalizedWallJumpDirection.x * speed, normalizedWallJumpDirection.y * adjustedJumpForce);
        //Debug.Log("Wall Jump Direction:" + normalizedWallJumpDirection);
        
        canWallJump = false;
        temporarilyDisableWallDetection = true;
        isWallSliding = false;
        Invoke(nameof(EnableWallDetection), wallJumpCooldown);
        Invoke(nameof(EnableMovement), wallJumpCooldown);
    }

    private void EnableMovement()
    {
        canMove = true;
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

    private void HandleAnimations()
    {
        animator.SetFloat("yVelocity", rb.velocity.y);
        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("isMoving", rb.velocity.x != 0);
        animator.SetBool("isWallSliding", isWallSliding);
    }
    public void TriggerDoubleJump()
    {
        animator.SetTrigger("isDoubleJumping");
    }
    public void TriggerHitAnimation()
    {
        animator.SetTrigger("Hit");
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance,
            wallCheck.position.y, 
            wallCheck.position.z));
    }
}
