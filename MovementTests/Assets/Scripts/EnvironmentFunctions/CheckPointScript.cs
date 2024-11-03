using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointScript : MonoBehaviour
{
    public Transform checkpointPosition;
    private Respawn respawnScript;
    private Animator animator;
    
    void Start()
    {
        respawnScript = FindObjectOfType<Respawn>();  
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(TriggerCheckpointSequence());
            respawnScript.SetCheckpoint(checkpointPosition.position);
        }
    }

    private IEnumerator TriggerCheckpointSequence()
    {
        TriggerUnfurlAnimation();
        yield return new WaitForSeconds(1f);
        TriggerOpenedAnimation();
    }

    private void TriggerUnfurlAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("Unfurl");
        }
        else
        {
            Debug.LogWarning("Animator not found (Unfurl)");
        }
    }
    private void TriggerOpenedAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("Opened");
        }
        else
        {
            Debug.LogWarning("Animator not found (Opened)");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
