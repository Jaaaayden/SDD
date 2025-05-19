using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;

public class TriggerSnap : MonoBehaviour
{
    private SnapZone currentZone = null;
    private XRGrabInteractable grab;
    private Transform interactorTransform;

    private void Awake()
    {
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
            transform.position = currentZone.snapPoint.position + new Vector3(0, transform.GetChild(0).GetComponent<Collider>().bounds.extents.y, transform.GetChild(0).GetComponent<Collider>().bounds.extents.z);
            transform.rotation = currentZone.snapPoint.parent.rotation;
        }
    }

    /*private void Update()
    {
       
    }*/
}