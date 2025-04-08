using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class InstantiateOnGrab : MonoBehaviour
{
    [Tooltip("The prefab to instantiate when this object is grabbed")]
    public GameObject prefabToInstantiate;
    
    [Tooltip("Where to create the new object")]
    public Transform spawnPoint;
    
    [Tooltip("Should the instantiated object be automatically selected by the grabbing interactor?")]
    public bool autoSelectInstantiatedObject = true;
    
    [Tooltip("Should the object be pulled to the hand after ray grab?")]
    public bool pullToHand = true;
    
    [Tooltip("How quickly to pull the object to hand (if enabled)")]
    public float pullSpeed = 10f;
    
    private XRGrabInteractable grabInteractable;
    
    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        
        if (grabInteractable == null)
        {
            Debug.LogError("InstantiateOnGrabWithRaySupport requires an XRGrabInteractable component!");
            return;
        }
        
        grabInteractable.selectEntered.AddListener(OnGrabbed);
    }
    
    private void OnGrabbed(SelectEnterEventArgs args)
    {
        if (prefabToInstantiate == null)
        {
            Debug.LogWarning("No prefab assigned to instantiate!");
            return;
        }
        
        // Get the interactor
        var interactor = args.interactorObject as XRBaseInteractor;
        if (interactor == null) return;
        
        // Determine if this is a ray interactor
        bool isRayInteractor = interactor is XRRayInteractor;
        
        // Determine spawn position & rotation
        Vector3 spawnPosition;
        Quaternion spawnRotation;
        
        if (spawnPoint)
        {
            spawnPosition = spawnPoint.position;
            spawnRotation = spawnPoint.rotation;
        }
        else
        {
            spawnPosition = transform.position;
            spawnRotation = transform.rotation;
        }
        
        // Create the object
        GameObject instantiatedObject = Instantiate(prefabToInstantiate, spawnPosition, spawnRotation);
        
        if (autoSelectInstantiatedObject)
        {
            XRGrabInteractable newGrabbable = instantiatedObject.GetComponent<XRGrabInteractable>();
            
            if (newGrabbable != null)
            {
                // Configure the new grabbable based on interactor type
                ConfigureGrabbableForInteractor(newGrabbable, interactor, isRayInteractor);
                
                // Cancel current selection
                interactor.interactionManager.CancelInteractableSelection((IXRSelectInteractable)grabInteractable);
                
                // Then grab the new object
                StartCoroutine(SelectObjectNextFrame(interactor, newGrabbable, isRayInteractor));
            }
        }
    }
    
    private void ConfigureGrabbableForInteractor(XRGrabInteractable grabbable, XRBaseInteractor interactor, bool isRayInteractor)
    {
        // For ray interactors, we might want different movement settings
        if (isRayInteractor)
        {
            // Configure for ray-based grabbing
            if (pullToHand)
            {
                // Use kinematic for smooth motion to hand
                grabbable.movementType = XRBaseInteractable.MovementType.Kinematic;
                
                // Fast but smooth follow settings
                grabbable.smoothPosition = true;
                grabbable.smoothRotation = true;
                grabbable.smoothPositionAmount = pullSpeed;
                grabbable.smoothRotationAmount = pullSpeed;
                grabbable.tightenPosition = 0.5f;
                grabbable.tightenRotation = 0.5f;
                
                // Make sure we track position and rotation
                grabbable.trackPosition = true;
                grabbable.trackRotation = true;
            }
            else
            {
                // Keep at ray hit point
                grabbable.movementType = XRBaseInteractable.MovementType.Instantaneous;
                grabbable.trackPosition = false;
                grabbable.trackRotation = false;
                grabbable.smoothPosition = false;
                grabbable.smoothRotation = false;
            }
        }
        else
        {
            // For direct grab, use default dynamic attach settings
            grabbable.attachPointCompatibilityMode = XRGrabInteractable.AttachPointCompatibilityMode.Default;
            grabbable.movementType = XRBaseInteractable.MovementType.Instantaneous;
            grabbable.trackPosition = true;
            grabbable.trackRotation = true;
            grabbable.smoothPosition = true;
            grabbable.smoothRotation = true;
            grabbable.smoothPositionAmount = 5f;
            grabbable.smoothRotationAmount = 5f;
            grabbable.tightenPosition = 0.5f;
            grabbable.tightenRotation = 0.5f;
        }
    }
    
    private IEnumerator SelectObjectNextFrame(XRBaseInteractor interactor, XRGrabInteractable newGrabbable, bool isRayInteractor)
    {
        // Wait for next frame
        yield return null;
        
        // Get ray hit position and rotation if relevant
        Vector3 grabPosition = interactor.transform.position;
        Quaternion grabRotation = interactor.transform.rotation;
        
        if (isRayInteractor && interactor is XRRayInteractor rayInteractor)
        {
            // For ray interactors, get the actual hit point
            // Fixed the TryGetHitInfo method call with correct parameter types
            if (rayInteractor.TryGetHitInfo(out Vector3 hitPosition, out Quaternion hitRotation, out int _, out bool _))
            {
                grabPosition = hitPosition;
                grabRotation = hitRotation;
            }
            
            // If we want to pull to hand
            if (pullToHand)
            {
                // Create an attach point at the hit position initially
                GameObject attachPoint = new GameObject("Ray Attach Point");
                attachPoint.transform.SetParent(newGrabbable.transform, false);
                
                // Calculate local position of the hit point
                Vector3 localHitPoint = newGrabbable.transform.InverseTransformPoint(grabPosition);
                attachPoint.transform.localPosition = localHitPoint;
                
                // Set rotation
                Quaternion localHitRotation = Quaternion.Inverse(newGrabbable.transform.rotation) * grabRotation;
                attachPoint.transform.localRotation = localHitRotation;
                
                // Assign as attach transform
                newGrabbable.attachTransform = attachPoint.transform;
                
                // Create a component to move this to the hand over time
                var pullToHandScript = attachPoint.AddComponent<PullToHand>();
                pullToHandScript.Initialize(rayInteractor.transform, pullSpeed);
            }
        }
        else
        {
            // For direct grab, set the attach point where the controller is
            if (newGrabbable.attachTransform == null)
            {
                GameObject attachPoint = new GameObject("Direct Attach Point");
                attachPoint.transform.SetParent(newGrabbable.transform, false);
                
                // Calculate local position & rotation for the attachment point
                Vector3 localGrabPoint = newGrabbable.transform.InverseTransformPoint(grabPosition);
                attachPoint.transform.localPosition = localGrabPoint;
                
                Quaternion localGrabRotation = Quaternion.Inverse(newGrabbable.transform.rotation) * grabRotation;
                attachPoint.transform.localRotation = localGrabRotation;
                
                newGrabbable.attachTransform = attachPoint.transform;
            }
        }
        
        // Select the object
        interactor.interactionManager.SelectEnter(interactor, (IXRSelectInteractable)newGrabbable);
    }
    
    private void OnDestroy()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnGrabbed);
        }
    }
}

// Helper class to pull the attach point to the hand over time
public class PullToHand : MonoBehaviour
{
    private Transform targetTransform;
    private float pullSpeed;
    private bool isInitialized = false;
    
    public void Initialize(Transform target, float speed)
    {
        targetTransform = target;
        pullSpeed = speed;
        isInitialized = true;
    }
    
    void Update()
    {
        if (!isInitialized || targetTransform == null) return;
        
        // Move attach point toward the hand
        transform.position = Vector3.Lerp(transform.position, targetTransform.position, Time.deltaTime * pullSpeed);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetTransform.rotation, Time.deltaTime * pullSpeed);
    }
}