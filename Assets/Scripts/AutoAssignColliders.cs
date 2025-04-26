using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class AutoAssignColliders : MonoBehaviour
{
    private void Awake()
    {
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();

        grabInteractable.colliders.Clear();

        Collider[] parentColliders = GetComponents<Collider>();

        foreach (Collider col in parentColliders)
        {
            grabInteractable.colliders.Add(col);
        }
    }
}