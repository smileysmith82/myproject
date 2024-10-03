using UnityEngine;

public class CharacterAnimatorController : MonoBehaviour
{
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        HandleAnimations();
    }
    private void HandleAnimations()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        animator.SetBool("isRunning", horizontalInput != 0);

        if (Input.GetButtonDown("Jump"))
        {
            animator.SetTrigger("Jump");
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            animator.SetBool("isWallJumping", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collided with: " + collision.gameObject.name);
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
