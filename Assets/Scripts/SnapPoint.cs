using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SnapPoint : MonoBehaviour
{
    public float snapDistance = 0.8f;
    public float breakDistance = 0.15f;
    public float snapForce = 10f;
    
    private SnapPoint connectedPoint;
    private bool isSnapped = false;
    private XRGrabInteractable parentGrabbable;
    private Rigidbody parentRigidbody;
    
    void Start()
    {
        // Get reference to parent object's components
        parentGrabbable = GetComponentInParent<XRGrabInteractable>();
        parentRigidbody = GetComponentInParent<Rigidbody>();
    }
    
    void FixedUpdate()
    {
        if (isSnapped)
        {
            // Check if objects are being pulled apart
            if (Vector3.Distance(transform.position, connectedPoint.transform.position) > breakDistance && !IsBeingHeld())
            {
                DisconnectFromPoint();
            }
        }
        else
        {
            // Look for potential snap points when object is not being held
            if (!IsBeingHeld())
                FindAndSnapToPoint();
        }
    }
    
    private bool IsBeingHeld()
    {
        return parentGrabbable != null && parentGrabbable.isSelected;
    }
    
   private void FindAndSnapToPoint()
{
    // Find all snap points in the scene
    SnapPoint[] allPoints = FindObjectsOfType<SnapPoint>();
    Debug.Log($"Found {allPoints.Length} potential snap points");
    
    foreach (SnapPoint point in allPoints)
    {
        // Skip if this is ourselves or if the other point is already connected
        if (point == this || point.isSnapped || point.transform.root == transform.root)
        {
            Debug.Log($"Skipping point: {point.name} - self: {point == this}, already snapped: {point.isSnapped}, same object: {point.transform.root == transform.root}");
            continue;
        }
            
        // Check if within snap distance
        float distance = Vector3.Distance(transform.position, point.transform.position);
        Debug.Log($"Distance to {point.name}: {distance}, snap distance: {snapDistance}");
        if (distance <= snapDistance)
        {
            Debug.Log($"Attempting to connect to {point.name}");
            ConnectToPoint(point);
            break;
        }
    }
}
    
    private void ConnectToPoint(SnapPoint point)
    {
        connectedPoint = point;
        point.connectedPoint = this;
        
        isSnapped = true;
        point.isSnapped = true;
        
        // Create fixed joint to connect the objects
        FixedJoint joint = parentRigidbody.gameObject.AddComponent<FixedJoint>();
        joint.connectedBody = point.GetComponentInParent<Rigidbody>();
        joint.breakForce = snapForce;
        
        // Align positions
        parentRigidbody.position += point.transform.position - transform.position;
        
        // Optional: Play sound or visual effect
        Debug.Log("Objects snapped together");
    }
    
    private void DisconnectFromPoint()
    {
        if (connectedPoint != null)
        {
            connectedPoint.isSnapped = false;
            connectedPoint.connectedPoint = null;
        }
        
        isSnapped = false;
        
        // Remove the joint
        FixedJoint joint = parentRigidbody.GetComponent<FixedJoint>();
        if (joint != null)
            Destroy(joint);
            
        // Optional: Play disconnect sound/effect
        Debug.Log("Objects pulled apart");
    }
}
