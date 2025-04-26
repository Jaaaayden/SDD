using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;

public class TriggerSnap : MonoBehaviour
{
    private SnapZone currentZone = null;
    private XRGrabInteractable grab;
    private bool isHeld = false;
    private Transform interactorTransform;
    private Vector3 initialOffset;
    private float originalZ;
    public GameObject xrRig;
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
        isHeld = true;
        interactorTransform = args.interactorObject.transform;
        initialOffset = transform.position - interactorTransform.position;
        originalZ = transform.position.z;
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        isHeld = false;
        interactorTransform = null;

        if (currentZone != null)
        {
            transform.position = currentZone.snapPoint1.position;
            transform.rotation = currentZone.snapPoint1.rotation;
            if (soundClip5 != null)
            {
                AudioSource.PlayClipAtPoint(soundClip5, xrRig.transform.position);
            }
        }
    }

    private void Update()
    {
        if (isHeld && interactorTransform != null)
        {
            Vector3 targetWorldPos = interactorTransform.position + initialOffset;
            Vector3 clampedTarget = new Vector3(
                Mathf.Clamp(targetWorldPos.x, 175.4f, 194.6f),
                Mathf.Clamp(targetWorldPos.y, 86.09372f, 103.90628f),
                originalZ
            );

            transform.position = clampedTarget;
        }
    }
}