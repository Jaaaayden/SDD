using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class deleter : MonoBehaviour
{
    public AudioClip clipToPlay;  // Assign this in the Inspector
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (clipToPlay == null)
        {
            Debug.LogError("No audio clip assigned!");
            Destroy(this);
            return;
        }

        audioSource.clip = clipToPlay;
        audioSource.Play();

        Destroy(gameObject, clipToPlay.length);
    }
}


