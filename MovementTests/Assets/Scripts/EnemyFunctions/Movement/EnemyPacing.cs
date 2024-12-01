using UnityEngine;

public class EnemyPacing : MonoBehaviour
{
    public float speed = 1.0f;
    public float movementRange = 5.0f;
    private Vector2 startingPostition;

    private void Start()
    {
        startingPostition = transform.position;
    }
    private void Update()
    {
        float newX = startingPostition.x + Mathf.PingPong(Time.time * speed, movementRange);
        var newPosition = new Vector2(newX, startingPostition.y);
        transform.position = newPosition;

    }
}
