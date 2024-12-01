using UnityEngine;



public class Respawn : MonoBehaviour
{
    
    public Transform startPoint;
    private Transform currentRespawnPoint;
    private Player player;
    private Rigidbody2D rb;

    private void Start()
    {
        player = GetComponent<Player>();
        rb=GetComponent<Rigidbody2D>();
        currentRespawnPoint = startPoint;
    }
    public void RespawnPlayer()
    {
        transform.position = currentRespawnPoint.position;
        rb.velocity = Vector2.zero;
    }
    public void SetCheckpoint(Vector3 newCheckpoint)
    {
        currentRespawnPoint.position = newCheckpoint;
    }
}
 