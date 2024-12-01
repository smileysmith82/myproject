using UnityEngine;
using UnityEngine.Events;
public class SimpleTriggerBehaviour : MonoBehaviour
{
    public UnityEvent triggerEvent;

    private void OnTriggerEnter(Collider other)
    {
        triggerEvent.Invoke();
    }
}
