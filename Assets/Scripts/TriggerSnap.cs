using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TriggerSnap : MonoBehaviour
{
    private SnapZone currentZone = null;
    private XRGrabInteractable grab;
    private bool isHeld = false;
    private Transform interactorTransform;
    private Vector3 initialOffset;
    private float originalZ;

    void Awake()
    {
        grab = GetComponent<XRGrabInteractable>();
        grab.selectEntered.AddListener(OnGrab);
        grab.selectExited.AddListener(OnRelease);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out SnapZone zone))
        {
            currentZone = zone;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out SnapZone zone) && currentZone == zone)
        {
            currentZone = null;
        }
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        isHeld = true;
        interactorTransform = args.interactorObject.transform;
        initialOffset = transform.position - interactorTransform.position;
        originalZ = transform.position.z;
    }

    void OnRelease(SelectExitEventArgs args)
    {
        isHeld = false;
        interactorTransform = null;

        if (currentZone != null)
        {
            transform.position = currentZone.snapPoint.position;
            transform.rotation = currentZone.snapPoint.rotation;
        }
    }

    void Update()
    {
        if (isHeld && interactorTransform != null)
        {
            Vector3 targetWorldPos = interactorTransform.position + initialOffset;
            Vector3 clampedTarget = new Vector3(
                Mathf.Clamp(targetWorldPos.x, -14.6f, 4.6f),
                Mathf.Clamp(targetWorldPos.y, 1.46372f, 19.27628f),
                originalZ
            );

            transform.position = clampedTarget;
        }
    }
}