using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class ENGTriggerSnap : MonoBehaviour
{
    private ENGSnapZone currentZone = null;
    private XRGrabInteractable grab;
    private GameObject[] attachPoints;
    public TextMeshPro text;
    private void Awake()
    {
        grab = GetComponent<XRGrabInteractable>();
        grab.selectEntered.AddListener(OnGrab);
        grab.selectExited.AddListener(OnRelease);
        attachPoints = new GameObject[] {transform.GetChild(1).gameObject, transform.GetChild(2).gameObject};
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.TryGetComponent(out ENGSnapZone zone))
        {
            if (currentZone == null)
                currentZone = zone;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out ENGSnapZone zone) && currentZone == zone)
        {
            currentZone = null;
        }
    }

    private void OnGrab(SelectEnterEventArgs args)
    {

    }

    private void OnRelease(SelectExitEventArgs args)
    {

        if (currentZone != null)
        {
            int index = 1;
            Vector3[] distVectors = new Vector3[] {currentZone.snapPoint.position - attachPoints[0].transform.position, currentZone.snapPoint.position - attachPoints[1].transform.position};
            float distDiff = Vector3.Magnitude(distVectors[0]) - Vector3.Magnitude(distVectors[1]);
            if (distDiff<0)
                index = 0;
            transform.rotation = currentZone.snapPoint.parent.rotation;
            if (index == 1)
            {
                transform.Rotate(0, 180, 0, Space.Self);
            }
            distVectors = new Vector3[] {currentZone.snapPoint.position - attachPoints[0].transform.position, currentZone.snapPoint.position - attachPoints[1].transform.position};
            transform.position += distVectors[index];
             
        }
    }

    /*private void Update()
    {
       
    }*/
}