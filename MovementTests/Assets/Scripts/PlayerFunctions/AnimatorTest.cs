using UnityEngine;

public class AnimatorTest : MonoBehaviour
{
    private Player player;
    private Animator animator;
    private Rigidbody2D rb;
    void Start()
    {
        player = GetComponent<Player>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        UpdateAnimations();
    }

    public void UpdateAnimations()
    {
        animator.SetFloat("yVelocity", rb.velocity.y);
        animator.SetBool("isGrounded", player.isGrounded);
        animator.SetBool("isMoving", rb.velocity.x != 0);
        animator.SetBool("isWallSliding", player.isWallSliding);
    }
    public void TriggerDoubleJump()
    {
        animator.SetTrigger("isDoubleJumping");
    }
    public void TriggerHitAnimation()
    {
        animator.SetTrigger("Hit");
    }
}
