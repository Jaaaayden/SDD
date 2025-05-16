using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class disappearOnGrab : MonoBehaviour
{  
    private XRGrabInteractable grabInteractable;
    public Light directionalLight;
    public TextMeshProUGUI textshii;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnGrab);
        
    }

    private void OnGrab(SelectEnterEventArgs args){
        gameObject.SetActive(false);
        directionalLight.intensity = 0.2f;
        textshii.text = "Nice, safety is your first priority in the lab. Now, let's begin the lab! The trees you just collected can be harvested for bacteria. The CRISPR machine on the table to your right will scrape bacteria off of the leaves, and then allow us to modify the bacterial genes. This is the first step in creating superbacteria that we can then test our Professor's bacteria killing cure on!!";
    }
}
