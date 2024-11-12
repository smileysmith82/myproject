using UnityEngine;

public class SawMovement : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 5f;
    private int currentWaypointIndex = 0;
    
    void Update()
    {
        if (waypoints.Length == 0)
            return;

        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 direction = targetWaypoint.position - transform.position;
        transform.Translate(direction.normalized*speed*Time.deltaTime, Space.World);
        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }
}
