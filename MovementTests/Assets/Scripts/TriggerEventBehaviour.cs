using UnityEngine;
using UnityEngine.Events;

public class TriggerEventBehaviour : MonoBehaviour
{
    public UnityEvent triggerEvent;

    private void OnTriggerEnter2D (Collider2D other)
    {
        triggerEvent.Invoke();
        Debug.Log("Player interacted with the object");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
