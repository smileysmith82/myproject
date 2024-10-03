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

    public void RespawnPlayer()
    {
        transform.position = startPoint.position;
        rb.velocity = new Vector2(0,0);
        characterMover.speed += 5f;
    }
}
