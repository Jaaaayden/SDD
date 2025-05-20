using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class SnapPoint : MonoBehaviour
{
    [Header("Snap Settings")]
    public float snapDistance = 0.1f;
    public float breakDistance = 0.15f;
    public float snapForce = 10f;
    public bool showDebug = true;
    
    private XRGrabInteractable grabInteractable;
    private SnapObjectManager snapManager;
    

    void Awake()
    {   
        snapManager = GetComponentInParent<SnapObjectManager>();
        grabInteractable = GetComponentInParent<XRGrabInteractable>();
    }

    void OnTriggerStay(Collider other){
        SnapPoint otherSnapPoint = other.GetComponent<SnapPoint>();
        if (otherSnapPoint != null && !HasJoint(otherSnapPoint))
        {
            float distance = Vector3.Distance(transform.position, otherSnapPoint.transform.position);
            if (distance <= snapDistance)
            {
                snapManager.CreateJoint(otherSnapPoint.GetComponentInParent<Rigidbody>(), snapForce);
                StartCoroutine(ForceRegrab());
            }
        }
    }

    bool HasJoint(SnapPoint other){
        Rigidbody thisRB = GetComponentInParent<Rigidbody>();
        Rigidbody otherRB = other.GetComponentInParent<Rigidbody>();
        if (thisRB == otherRB) return false;

        foreach (Joint joint in thisRB.GetComponents<Joint>())
        {
            if (joint.connectedBody == otherRB)
                return true;
        }
        return false;
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
    void OnDrawGizmosSelected()
    {
        Gizmos.color =  Color.yellow;
        Gizmos.DrawWireSphere(transform.position, snapDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, breakDistance);
        
    }
}