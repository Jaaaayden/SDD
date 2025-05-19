using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;

public class TriggerSnap : MonoBehaviour
{
    private SnapZone currentZone = null;
    private XRGrabInteractable grab;
    private Transform interactorTransform;
    private Vector3 initialOffset;
    private float originalZ;
    public AudioClip soundClip5;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        grab = GetComponent<XRGrabInteractable>();
        grab.selectEntered.AddListener(OnGrab);
        grab.selectExited.AddListener(OnRelease);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.TryGetComponent(out SnapZone zone))
        {
            if (currentZone == null)
                currentZone = zone;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out SnapZone zone) && currentZone == zone)
        {
            currentZone = null;
        }
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        interactorTransform = args.interactorObject.transform;
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        interactorTransform = null;

        if (currentZone != null)
        {
            transform.position = currentZone.snapPoint.position;
            transform.rotation = currentZone.snapPoint.rotation;
            if (soundClip5 != null)
            {
                AudioSource.PlayClipAtPoint(soundClip5, interactorTransform.position);
            }
        }
    }

    /*private void Update()
    {
       
    }*/
}