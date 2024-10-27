using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

public class OneWayPlatformScript : MonoBehaviour
{
    public LayerMask platformLayerMask;
    public Collider2D playerCollider;
    private bool isCollidingWithPlatform = false;
    
    void Start()
    {
        playerCollider = GameObject.FindWithTag("Player").GetComponent<Collider2D>();
    }
    
    void Update()
    {
        HandlePlatformCollision();
    }

    private void HandlePlatformCollision()
    {
        if (isCollidingWithPlatform && Input.GetKeyDown(KeyCode.S) )
        {
            StartCoroutine(DisableCollisionTemporarily());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & platformLayerMask) != 0)
        {
            isCollidingWithPlatform = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & platformLayerMask) != 0)
        {
            isCollidingWithPlatform = false;
        }
    }


    private IEnumerator DisableCollisionTemporarily()
    {
        Physics2D.IgnoreLayerCollision(playerCollider.gameObject.layer, platformLayerMask, true);
        yield return new WaitForSeconds(0.2f);
        Physics2D.IgnoreLayerCollision(playerCollider.gameObject.layer, platformLayerMask, false);

    }
    
}
