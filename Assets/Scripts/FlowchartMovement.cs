using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class FlowchartMovement : MonoBehaviour
{
    public InputActionReference buttonA;
    public InputActionReference buttonB;
    public BoxCollider zoneCollider;
    public GameObject xrRig;
    public ActionBasedContinuousMoveProvider moveProvider;
    private bool inTriggerZone = false;

    private void OnEnable()
    {
        buttonA.action.Enable();
        buttonB.action.Enable();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inTriggerZone = true;
            if (moveProvider != null)
            {
                moveProvider.useGravity = false; 
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inTriggerZone = false; 
            if (moveProvider != null)
            {
                moveProvider.useGravity = true; 
            }
        }
    }

    private void Start()
    {
        OnEnable();
    }

    private void Update() // gpt assisted
    {
        if (!inTriggerZone || xrRig == null || zoneCollider == null) return;

        float moveY = 0f;

        if (buttonB.action.IsPressed())
            moveY += 1.5f * Time.deltaTime;

        if (buttonA.action.IsPressed())
            moveY -= 1.5f * Time.deltaTime;

        Vector3 newPosition = xrRig.transform.position + new Vector3(0, moveY, 0);

        Bounds bounds = zoneCollider.bounds;
        newPosition.y = Mathf.Clamp(newPosition.y, bounds.min.y, bounds.max.y);

        xrRig.transform.position = newPosition;
    }
}