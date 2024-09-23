using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    private  Vector3 offset = new Vector3(3, 3, -5);

    void LateUpdate()
    {
        transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, offset.z);
    }
    
}
