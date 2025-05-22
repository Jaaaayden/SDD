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
    public int snapForce = 500;
    public SnapObjectManager snapManager;
    private XRGrabInteractable grabInteractable;
    private void Awake()
    {
        grab = GetComponent<XRGrabInteractable>();
        grab.selectEntered.AddListener(OnGrab);
        grab.selectExited.AddListener(OnRelease);
        attachPoints = new GameObject[] { transform.GetChild(1).gameObject, transform.GetChild(2).gameObject };
        grabInteractable = GetComponentInParent<XRGrabInteractable>();
        snapManager = GetComponentInParent<SnapObjectManager>();


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
            //get the distances between each snap point and the attach point
            Vector3[] distVectors = new Vector3[] { currentZone.snapPoint.position - attachPoints[0].transform.position, currentZone.snapPoint.position - attachPoints[1].transform.position };
            //check which distance is smaller
            float distDiff = Vector3.Magnitude(distVectors[0]) - Vector3.Magnitude(distVectors[1]);
            if (distDiff < 0)
                index = 0;
            //ensure object faces the direction closer to its initial direction
            Quaternion initialRotation = transform.rotation;
            transform.rotation = currentZone.snapPoint.parent.rotation;
            if (Quaternion.Angle(yRotation(initialRotation), yRotation(currentZone.snapPoint.parent.rotation)) > 90)
                transform.Rotate(0, 180, 0, Space.Self);
            //find the new distance vectors
            distVectors = new Vector3[] { currentZone.snapPoint.position - attachPoints[0].transform.position, currentZone.snapPoint.position - attachPoints[1].transform.position };
            //move the parent object along the distance vector (making the new distance between the snap point and attach point 0)
            transform.position += distVectors[index];
            //create a fixed joint between the snapped objects so they move together
            snapManager.CreateJoint(currentZone.snapPointObj.GetComponentInParent<Rigidbody>(), snapForce);
            StartCoroutine(ForceRegrab());

        }
    }

    /*private void Update()
    {
       
    }*/
    private Quaternion yRotation(Quaternion q)
    {
        float theta = Mathf.Atan2(q.y, q.w);
        // quaternion representing rotation about the y axis
        return new Quaternion(0, Mathf.Sin(theta), 0, Mathf.Cos(theta));
    }

    IEnumerator ForceRegrab(){
        if (grabInteractable.isSelected)
        {
            // Force release the object
            var interactor = grabInteractable.interactorsSelecting[0];
            grabInteractable.interactionManager.SelectExit(interactor, grabInteractable);

            // Wait one frame
            yield return null;

            // Re-grab
            grabInteractable.interactionManager.SelectEnter(interactor, grabInteractable);
        }
    }
    
    
}