using UnityEngine;

public class CharacterAnimatorControllerTrig : MonoBehaviour
{
    private Animator animator;
    private CharacterMover characterMover;
    private Rigidbody2D rb;

    private void Start()
    {
        animator = GetComponent<Animator>();
        characterMover = GetComponent<CharacterMover>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HandleAnimations();
    }

    private void HandleAnimations()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        if (characterMover.isGrounded == true)
        {
            animator.SetBool("isRunning", horizontalInput != 0);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        if (rb.velocity.y < -0.1f && characterMover.isGrounded != true)
        {
            animator.SetBool("isFalling", true);
        }
        else
        {
            animator.SetBool("isFalling", false);
        }

        if (Input.GetButtonDown("Jump"))
        {
            animator.SetTrigger("Jump");
        }

        if (characterMover.HasDoubleJump() && characterMover.GetCurrentJumpCount() == 2 && Input.GetButtonDown("Jump"))
        {
            animator.SetTrigger("DoubleJump");
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            animator.SetTrigger("WallJump");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Collided with: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            HitEnemy();
        }
    }

    private void HitEnemy()
    {
        animator.SetBool("isHit", true);
        Debug.Log("Hit an enemy");
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            animator.SetBool("isHit", false);
        }
    }
}
