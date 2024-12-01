using UnityEngine;

public class RollingScript : MonoBehaviour
{
    [Header("MovementSettings")]
    public float sideSpeed = 1f;
    public float movementRange = 5.0f;
    public float rotationSpeed = 180f;
    
    private Vector3 startingPosition;
    private float lastXPosition;

    private void Start()
    {
        startingPosition = transform.position;
        lastXPosition = startingPosition.x;
    }
    // Update is called once per frame
    void Update()
    {
        float newX = startingPosition.x + Mathf.PingPong(Time.time * sideSpeed, movementRange);
        transform.position = new Vector3(newX, startingPosition.y, startingPosition.z);
        float direction = Mathf.Sign(newX - lastXPosition);
        transform.Rotate(Vector3.forward, -rotationSpeed * Time.deltaTime*direction);
        lastXPosition = newX;
    }
}
