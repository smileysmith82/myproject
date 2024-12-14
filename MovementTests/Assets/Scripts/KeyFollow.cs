using UnityEngine;

public class KeyFollow : MonoBehaviour
{
    public Transform followTarget;
    public ID requiredID;
    public  Vector3 offset = new Vector3(-1, -1, -1);
    private bool isFollowing = false;
    private Player player;
    
    private void Start()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if (isFollowing && followTarget != null)
        {
            transform.position = Vector2.Lerp(transform.position, (Vector2)followTarget.position + (Vector2)offset, 5f * Time.deltaTime);
        }

        DetectTraps();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isFollowing = true;
            followTarget = other.transform;
        }
        var idBehaviour = other.GetComponent<SimpleIDBehaviour>();
        if (isFollowing && idBehaviour != null && idBehaviour.id == requiredID)
        {
            isFollowing = false;
            followTarget = null;
            Debug.Log("Key delivered to correct object!");
            
            
            Destroy(gameObject);

            DeactivateTrap(other);
        }
    }

    private void DetectTraps()
    {
        if (player != null)
        {
            Collider2D[] trapsInRange = Physics2D.OverlapCircleAll(player.trapDetection.position, player.trapDetectionRadius);
            foreach (var trap in trapsInRange)
            {
                if (trap.CompareTag("TrapBase"))
                {
                    var idBehaviour = trap.GetComponent<SimpleIDBehaviour>();
                    if (idBehaviour != null && idBehaviour.id == requiredID)
                    {
                        Debug.Log("ID matched with Trap Base. Disarming the trap.");
                        DeactivateTrap(trap);
                    }
                }
            }
        }
            
    }

    private void DeactivateTrap(Collider2D trapCollider)
    {
        trapCollider.gameObject.layer = LayerMask.NameToLayer("Default");
        trapCollider.gameObject.tag="Untagged";
        
        Debug.Log("Trap Deactivated");
    }
}
