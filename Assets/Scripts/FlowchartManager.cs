using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;
using TMPro;

public class FlowchartManager : MonoBehaviour
{
    public Transform xrRig;
    private AudioSource audioSource;
    public AudioClip tree;
    public GameObject box;
    private int totalConnectionsNeeded = 6;
    private int currentConnections = 0;
    private bool complete = false;

    private void Update()
    {
        if (audioSource != null && xrRig != null)
        {
            audioSource.transform.position = xrRig.position;
        }
    }

    public void RegisterConnection()
    {
        currentConnections++;

        if (currentConnections >= totalConnectionsNeeded)
        {
            PuzzleComplete();
        }
    }

    public void UnregisterConnection()
    {
        currentConnections = Mathf.Max(0, currentConnections - 1);
    }

    private void PuzzleComplete()
    {
        if (xrRig != null && !complete)
        {
            complete = true;

            xrRig.position = Vector3.zero;

            box.SetActive(false);

            GameObject audioObject = new GameObject("MovingAudioSource"); // gpt idea
            audioSource = audioObject.AddComponent<AudioSource>();

            audioSource.clip = tree;
            audioSource.spatialBlend = 1.7f;
            audioSource.Play();
        }
    }

    public void SetTotalConnections(int total)
    {
        totalConnectionsNeeded = total;
    }
}