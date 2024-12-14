using UnityEngine;

public class DisableScriptOnTrigger : MonoBehaviour
{
    public MonoBehaviour scriptToDisable;
    public GameObject targetObject;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Ensure the trigger is activated by the player
        {
            if (targetObject != null)
            {
                // Immediately reset the y-rotation to 0
                targetObject.transform.rotation = Quaternion.Euler(0, 0, targetObject.transform.rotation.z);
            }
            scriptToDisable.enabled = false; // Disable the script
            
        }
    }
}
