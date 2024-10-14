using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    
    [Header("Movement info")]
    [SerializeField] private float speed = 5;
    [SerializeField] private float jumpForce = 7f;
    private bool canDoubleJump;
    private bool canWallSlide;
    private bool isWallSliding;
    private float movingInput;
    private bool canMove = true;
    
    [Header("Collision info")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask whatIsGround;
                     private bool isGrounded;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private bool isWallDetected;
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput(); 
        ColliderCheck();
        AnimatorController();
    }
    private void FixedUpdate()
    {
        if (isGrounded)
            canDoubleJump = true;
        
        if (isWallDetected && canWallSlide)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x , rb.velocity.y * .75f);
        }
        else
        {
            isWallSliding = false;
            Move();
        }
    }
    private void JumpButton()
    {
        if (isGrounded)
        {
            Jump();
        }
        else if (canDoubleJump)
        {
            canDoubleJump = false;
            Jump();
        }
    }
    private void Jump()
    { 
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);    
    }


    private void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpButton();
        }
        movingInput = Input.GetAxis("Horizontal");
    }

    private void Move()
    {
        if (canMove)
        {
            rb.velocity = new Vector2(movingInput * speed, rb.velocity.y);
        }
    }
    private void AnimatorController()
    {
        bool isMoving = rb.velocity.x != 0;
        
        animator.SetFloat("yVelocity",rb.velocity.y);
        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("isMoving", isMoving);
        animator.SetBool("isWallSliding", isWallSliding);
    }

    private void ColliderCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        
        Vector2 wallCheckDirection = movingInput > 0 ? Vector2.right : Vector2.left;
        isWallDetected = Physics2D.Raycast(wallCheck.position, wallCheckDirection, wallCheckDistance, whatIsGround);
        if (!isGrounded && rb.velocity.y < 0 & isWallDetected)
        {
            canWallSlide = true;
        }
        else
        {
            canWallSlide = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance,
                                                        wallCheck.position.y, 
                                                        wallCheck.position.z));
    }
}
