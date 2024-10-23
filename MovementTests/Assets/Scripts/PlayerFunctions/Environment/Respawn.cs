using UnityEngine;
public class Respawn : MonoBehaviour
{
    
    public Transform startPoint;
    private Player player;
    private Rigidbody2D rb;

    private void Start()
    {
        player = GetComponent<Player>();
        rb=GetComponent<Rigidbody2D>();
    }

    public void RespawnPlayer()
    {
        transform.position = startPoint.position;
        rb.velocity = new Vector2(0,0);
    }
}
