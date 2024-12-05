using UnityEngine;

[RequireComponent(typeof(ParticleSystem), typeof(Collider2D))]
public class TriggerParticleEffect : MonoBehaviour
{
    private ParticleSystem particleSystem;

    public int particleAmount = 10;

    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            particleSystem.Emit(particleAmount);
        }
    }
}
