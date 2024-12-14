using System;
using UnityEngine;

public class AttachOnTrigger : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        transform.parent = other.transform;
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        transform.parent = null;
    }

    
}
