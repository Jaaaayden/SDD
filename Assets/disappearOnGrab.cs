using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class disappearOnGrab : MonoBehaviour
{  
    private XRGrabInteractable grabInteractable;
    void Start()
    {
        
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnGrab);
        
    }
    private void OnGrab(SelectEnterEventArgs args){
        gameObject.SetActive(false);
    }
}
