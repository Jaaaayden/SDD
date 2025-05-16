using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip intro;
    public Transform xrRig;

    private void Start()
    {
        if (intro != null && xrRig != null)
        {
            GameObject audioObject = new GameObject("MovingAudioSource"); // gpt idea
            audioSource = audioObject.AddComponent<AudioSource>();

            audioSource.clip = intro;
            audioSource.spatialBlend = 1.0f;
            audioSource.Play();
        }
    }

    private void Update()
    {
        if (audioSource != null && xrRig != null)
        {
            audioSource.transform.position = xrRig.position;
        }
    }
}