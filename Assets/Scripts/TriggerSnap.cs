using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class TriggerSnap : MonoBehaviour
{
    private SnapZone currentZone = null;
    private XRGrabInteractable grab;
    private Transform interactorTransform;
    public TextMeshPro text;
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
            Vector3 offset = Vector3.zero;
            try
            {

                // Use localScale instead of collider bounds to avoid rotation affecting extents
                Vector3 localExtents = transform.GetChild(0).GetComponent<Collider>().bounds.size * 0.5f;
                offset = new Vector3(localExtents.x, localExtents.y * 2, 0);
                text.text = offset.ToString();
            }
            catch (System.Exception e)
            {
                text.text = e.ToString();
            }
            transform.position = currentZone.snapPoint.position + offset;
            transform.rotation = currentZone.snapPoint.parent.rotation; 
        }
    }

    /*private void Update()
    {
       
    }*/
}