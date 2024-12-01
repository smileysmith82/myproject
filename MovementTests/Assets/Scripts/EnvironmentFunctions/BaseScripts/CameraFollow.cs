using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public  Vector3 offset = new Vector3(3, 3, -5);

    void Start()
    {
        if (player == null)
        {
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
            else
            {
                Debug.LogWarning("No player name tage found");
            }
            
        }
    }
    void LateUpdate()
    {
        if (player != null)
        {
            transform.position = new Vector3(
                player.position.x + offset.x,
                player.position.y + offset.y,
                offset.z);
        }
        else
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
        }
        
    }
    
}
