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
    
    private XRGrabInteractable grabInteractable;
    
    void Awake()
    {
        // Get the XR Grab Interactable component
        grabInteractable = GetComponent<XRGrabInteractable>();
        
        if (grabInteractable == null)
        {
            Debug.LogError("InstantiateOnGrab requires an XRGrabInteractable component!");
            return;
        }
        
        // Subscribe to the select entered event (when grabbing happens)
        grabInteractable.selectEntered.AddListener(OnGrabbed);
    }
    
    private void OnGrabbed(SelectEnterEventArgs args)
    {
        // Only proceed if we have a prefab to instantiate
        if (prefabToInstantiate == null)
        {
            Debug.LogWarning("No prefab assigned to instantiate!");
            return;
        }
        
        // Determine where to spawn the new object
        Vector3 spawnPosition = spawnPoint ? spawnPoint.position : transform.position;
        Quaternion spawnRotation = spawnPoint ? spawnPoint.rotation : transform.rotation;
        
        // Create the new object
        GameObject instantiatedObject = Instantiate(prefabToInstantiate, spawnPosition, spawnRotation);
        
        // If auto-select is enabled, make the interactor grab the new object
        if (autoSelectInstantiatedObject)
        {
            // Get the XRGrabInteractable from the new object
            XRGrabInteractable newGrabbable = instantiatedObject.GetComponent<XRGrabInteractable>();
            
            if (newGrabbable != null)
            {
                // Get the interactor that grabbed this object
                XRBaseInteractor interactor = args.interactorObject as XRBaseInteractor;
                
                if (interactor != null)
                {
                    // First cancel the current selection
                    interactor.interactionManager.CancelInteractableSelection((IXRSelectInteractable)grabInteractable);
                    
                    // Then select the new object - needs to happen next frame
                    StartCoroutine(SelectObjectNextFrame(interactor, newGrabbable));
                }
            }
        }
    }
    
    private IEnumerator SelectObjectNextFrame(XRBaseInteractor interactor, XRGrabInteractable newGrabbable)
    {
        // Wait for the next frame
        yield return null;
        
        // Select the new object with the interactor
        interactor.interactionManager.SelectEnter(interactor, (IXRSelectInteractable)newGrabbable);
    }
    
    private void OnDestroy()
    {
        // Unsubscribe when this object is destroyed
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnGrabbed);
        }
    }
}