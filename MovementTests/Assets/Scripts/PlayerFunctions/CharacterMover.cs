using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    public float speed = 5f;
    public float jumpValue = 7f;
    public LayerMask groundLayer;
    public LayerMask wallLayer;
    public int maxJumps = 1;
    public bool hasDoubleJump = false;
    
    private Rigidbody2D rb;
    private int jumpCount = 0;
    public bool isGrounded;

    void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
    }       
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(moveHorizontal*speed, rb.velocity.y);
        rb.velocity = movement;
        
        if (hasDoubleJump)
        {
            maxJumps = 2;
        }
        else
        {
            maxJumps = 1;
        }

        if (Input.GetButtonDown("Jump") && jumpCount < maxJumps)
        {
            Debug.Log("Current Jump Count: " + jumpCount);
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(new Vector2(0f, jumpValue), ForceMode2D.Impulse);
            jumpCount++;
        }

        if (IsGrounded())
        {
            jumpCount = 0;
        }
    }

    public bool CanJump()
    {
        return jumpCount < maxJumps;
    }
    public int GetCurrentJumpCount()
    {
        return jumpCount;
    }

    private bool IsGrounded()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.1f, groundLayer);
        return colliders.Length > 0; // True if there are any colliders on the ground layer
    }

    public bool IsTouchingWall()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.1f, wallLayer);
        return colliders.Length > 0;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true;
            jumpCount = 0;
        }

        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = false;
        }
    }

    public bool HasDoubleJump()
    {
        return hasDoubleJump;
    }

    public void EnableDoubleJump()
    {
        hasDoubleJump = true;
    }

    public void DisableDoubleJump()
    {
        hasDoubleJump = false;
    }
}
