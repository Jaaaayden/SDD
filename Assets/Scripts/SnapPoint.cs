using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SnapPoint : MonoBehaviour
{
    [Header("Snap Settings")]
    public float snapDistance = 0.1f;
    public float breakDistance = 0.15f;
    public float snapForce = 150f;
    public bool showDebug = true;

    [Header("References")]
    public XRGrabInteractable parentGrabbable;
    public Rigidbody parentRigidbody;

    private SnapPoint connectedPoint;
    private bool isSnapped = false;
    private FixedJoint joint;

    [Header("Sound Effects")]

    public AudioSource audioSource;
    public AudioClip connectSFX;
    public AudioClip disconnectSFX;

    void Awake()
    {   
        audioSource = GetComponent<AudioSource>();
        if (parentGrabbable == null)
            parentGrabbable = GetComponentInParent<XRGrabInteractable>();
        
        if (parentRigidbody == null)
            parentRigidbody = GetComponentInParent<Rigidbody>();

        if (showDebug)
        {
            Debug.Log($"[SnapPoint] Initialized on {gameObject.name}", this);
            Debug.Log($"[SnapPoint] Parent Grabbable: {(parentGrabbable != null ? parentGrabbable.name : "NULL")}", this);
            Debug.Log($"[SnapPoint] Parent Rigidbody: {(parentRigidbody != null ? parentRigidbody.name : "NULL")}", this);
        }
    }

    void FixedUpdate()
    {
        if (isSnapped)
        {
            if (connectedPoint == null)
            {
                if (showDebug) Debug.LogWarning("[SnapPoint] Connected point is null but isSnapped is true", this);
                DisconnectFromPoint();
                return;
            }

            float currentDistance = Vector3.Distance(transform.position, connectedPoint.transform.position);
            if (currentDistance > breakDistance && !IsBeingHeld())
            {
                if (showDebug) Debug.Log($"[SnapPoint] Breaking connection - Distance: {currentDistance}", this);
                DisconnectFromPoint();
            }
        }
        else if (!IsBeingHeld())
        {
            FindAndSnapToPoint();
        }
    }

    private bool IsBeingHeld()
    {
        bool isHeld = parentGrabbable != null && parentGrabbable.isSelected;
        if (showDebug && isHeld) Debug.Log($"[SnapPoint] Object is being held", this);
        return isHeld;
    }

    private void FindAndSnapToPoint()
    {
        SnapPoint[] allPoints = FindObjectsOfType<SnapPoint>();
        if (showDebug) Debug.Log($"[SnapPoint] Found {allPoints.Length} potential snap points", this);

        foreach (SnapPoint point in allPoints)
        {
            if (point == this || point.isSnapped || point.transform.parent == transform.parent)
            {
                if (showDebug) Debug.Log($"[SnapPoint] Skipping {point.name} - same object or already snapped", this);
                continue;
            }

            float distance = Vector3.Distance(transform.position, point.transform.position);
            if (showDebug) Debug.Log($"[SnapPoint] Distance to {point.name}: {distance}", this);

            if (distance <= snapDistance)
            {
                if (showDebug) Debug.Log($"[SnapPoint] Within snap distance of {point.name}", this);
                ConnectToPoint(point);
                return;
            }
        }
    }

    private void ConnectToPoint(SnapPoint point)
    {
        if (point == null)
        {
            if (showDebug) Debug.LogError("[SnapPoint] Attempted to connect to null point", this);
            return;
        }
        
        connectedPoint = point;
        point.connectedPoint = this;

        //Remove constraints when snapping
        parentRigidbody.constraints = RigidbodyConstraints.None;
        point.parentRigidbody.constraints = RigidbodyConstraints.None;

        isSnapped = true;
        point.isSnapped = true;

        // Calculate position offset
        Vector3 positionOffset = point.transform.position - transform.position;
        parentRigidbody.position += positionOffset;

        // Create joint
        joint = parentRigidbody.gameObject.AddComponent<FixedJoint>();
        joint.connectedBody = point.parentRigidbody;
        joint.breakForce = snapForce;
        joint.enableCollision = true;
        

        // Play sound effect
        audioSource.PlayOneShot(connectSFX);

        if (showDebug)
        {
            Debug.Log($"[SnapPoint] Successfully connected to {point.name}", this);
            Debug.Log($"[SnapPoint] Position offset: {positionOffset}", this);
            Debug.Log($"[SnapPoint] Created joint between {parentRigidbody.name} and {point.parentRigidbody.name}", this);
        }
    }

    private void DisconnectFromPoint()
    {
        // Restore constraints when unsnapping
        if (connectedPoint != null && connectedPoint.parentRigidbody != null)
            connectedPoint.parentRigidbody.constraints = RigidbodyConstraints.FreezeAll;

        if (parentRigidbody != null)
            parentRigidbody.constraints = RigidbodyConstraints.FreezeAll;

        if (connectedPoint != null)
        {
            connectedPoint.isSnapped = false;
            connectedPoint.connectedPoint = null;
        }
        
        isSnapped = false;

        if (joint != null)
        {
            Destroy(joint);
            joint = null;
        }

        //Play sound effect
        audioSource.PlayOneShot(disconnectSFX);

        if (showDebug) Debug.Log("[SnapPoint] Disconnected from point", this);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = isSnapped ? Color.green : Color.yellow;
        Gizmos.DrawWireSphere(transform.position, snapDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, breakDistance);
        
        if (isSnapped && connectedPoint != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, connectedPoint.transform.position);
        }
    }
}