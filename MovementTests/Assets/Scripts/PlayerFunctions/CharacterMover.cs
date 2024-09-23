using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    public float speed = 5f;
    public float jumpValue = 7f;
    public LayerMask groundLayer;
    public int maxJumps = 1;
    public bool hasDoubleJump = false;
    
    private Rigidbody2D rb;
    private int jumpCount = 0;
    private bool isGrounded;

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
    
    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, .51f, groundLayer);
        Debug.DrawRay(transform.position, Vector2.down * .51f, Color.blue);
        return hit.collider != null;

        
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
