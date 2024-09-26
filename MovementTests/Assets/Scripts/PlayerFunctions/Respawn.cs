using UnityEngine;
public class Respawn : MonoBehaviour
{
    
    public Transform startPoint;
    private CharacterMover characterMover;
    private Rigidbody2D rb;

    private void Start()
    {
        characterMover = GetComponent<CharacterMover>();
        rb=GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Collision detected with " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            
            transform.position = startPoint.position;
            rb.velocity = new Vector2(15f, 2f);
            characterMover.speed += 5f;
            
            
        }
    }   
}
