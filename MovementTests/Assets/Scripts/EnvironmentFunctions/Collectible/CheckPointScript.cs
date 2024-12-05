using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem), typeof(Collider2D))]
public class CheckPointScript : MonoBehaviour
{
    public Transform checkpointPosition;
    private Respawn respawnScript;
    private Animator animator;
    public AudioClip checkpointSound;
    private AudioSource audioSource;
    private bool checkpointActivated = false;
    private ParticleSystem particleSystem;
    public int particleAmount = 25;
    
    void Start()
    {
        
        respawnScript = FindObjectOfType<Respawn>();  
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        particleSystem = GetComponent<ParticleSystem>();
        
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !checkpointActivated)
        {
            checkpointActivated = true;
            StartCoroutine(TriggerCheckpointSequence());
            respawnScript.SetCheckpoint(checkpointPosition.position);
            PlayCheckPointSound();
            TriggerParticles();
        }
    }

    private void TriggerParticles()
    {
        particleSystem.Emit(particleAmount);
    }
    private IEnumerator TriggerCheckpointSequence()
    {
        TriggerUnfurlAnimation();
        yield return new WaitForSeconds(1f);
        TriggerOpenedAnimation();
    }

    private void PlayCheckPointSound()
    {
        if (checkpointSound != null)
        {
            audioSource.PlayOneShot(checkpointSound);
        }
        else
        {
            {
                Debug.LogWarning("Checkpoint sound not assigned");
            }
        }
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
}
