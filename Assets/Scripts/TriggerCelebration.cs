using UnityEngine;

public class TriggerCelebration : MonoBehaviour
{
    private AudioSource audioSource;
    public Transform xrRig;
    public ParticleSystem particles;
    public AudioClip yippee;
    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true;

            if (yippee != null)
                AudioSource.PlayClipAtPoint(yippee, xrRig.position);

            if (particles != null)
                particles.Play();
        }
    }
}