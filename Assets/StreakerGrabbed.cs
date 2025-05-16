using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class StreakerGrabbed : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    public TextMeshProUGUI textshii;
    // Start is called before the first frame update
    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(grabbedStreaker);
    }

    // Update is called once per frame
    void grabbedStreaker(SelectEnterEventArgs args){
    }
}
