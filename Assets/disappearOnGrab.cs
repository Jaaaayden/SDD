using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class disappearOnGrab : MonoBehaviour
{
    private Collider objectCollider;
    private XRGrabInteractable grabInteractable;
    // Start is called before the first frame update
    void Start()
    {
        objectCollider = GetComponent <Collider>();
        grabInteractable = GetComponent<XRGrabInteractable>();

        
    }
    private void OnGrab(SelectEnterEventArgs args){
        gameObject.SetActive(false);
    }
}
