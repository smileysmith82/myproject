using UnityEngine;
using UnityEngine.Events;

public class SimpleIDMatchBehaviour : MonoBehaviour
{
    public ID id;
    public UnityEvent matchEvent, noMatchEvent;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var otherID = other.GetComponent<SimpleIDBehaviour>();
        if (otherID.id == id)
        {
            matchEvent.Invoke();
            Debug.Log("Matched ID: " + id);
        }
        else
        {
            noMatchEvent.Invoke();
            Debug.Log("No match ID: " + id);
        }
    }
}
