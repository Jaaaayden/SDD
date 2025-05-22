using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.XR.Interaction.Toolkit;

public class VRLadderClimb : MonoBehaviour
{
    public Transform ladderTop;
    public Transform platformTarget;
    public GameObject xrRig;

    public float climbSpeed = 1.5f;
    public float walkDuration = 0.75f;
    public float lockDuration = 1.0f;

    public InputActionReference ButtonY;
    public InputActionReference ButtonB;

    public ActionBasedContinuousMoveProvider moveProvider;
    public ActionBasedContinuousTurnProvider turnProvider;

    private bool isClimbing = false;
    private bool cameraLocked = false;

    public AudioClip soundClip6;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = xrRig.AddComponent<AudioSource>();
        audioSource.clip = soundClip6;
    }

    private void OnEnable()
    {
        if (ButtonY != null) ButtonY.action.Enable();
        if (ButtonB != null) ButtonB.action.Enable();
    }

    private void OnDisable()
    {
        if (ButtonY != null) ButtonY.action.Disable();
        if (ButtonB != null) ButtonB.action.Disable();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isClimbing)
        {
            StartClimbing();
        }
    }

    private void StartClimbing()
    {
        isClimbing = true;
        LockCameraToLadder();
        DisableLocomotion();
    }

    private void LockCameraToLadder()
    {
        cameraLocked = true;

        Transform cameraTransform = Camera.main.transform;
        float cameraYaw = cameraTransform.eulerAngles.y;

        Vector3 flatDirection = ladderTop.position - cameraTransform.position;
        flatDirection.y = 0;

        Quaternion desiredFacing = Quaternion.LookRotation(flatDirection, Vector3.up);

        float desiredYaw = desiredFacing.eulerAngles.y;
        float yawOffset = desiredYaw - cameraYaw;

        Vector3 currentEuler = xrRig.transform.eulerAngles;
        Quaternion targetRotation = Quaternion.Euler(0f, currentEuler.y + yawOffset, 0f);

        StartCoroutine(SmoothRotate(targetRotation));
    }

    IEnumerator SmoothRotate(Quaternion targetRot)
    {
        Quaternion startRot = xrRig.transform.rotation;
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / lockDuration;
            xrRig.transform.rotation = Quaternion.Slerp(startRot, targetRot, t);
            yield return null;
        }
    }

    private void Update()
    {
        if (!isClimbing) return;

        bool yPressed = ButtonY != null && ButtonY.action.IsPressed();
        bool bPressed = ButtonB != null && ButtonB.action.IsPressed();
        Vector3 direction = (ladderTop.position - xrRig.transform.position).normalized;

        if (yPressed || bPressed)
        {
            xrRig.transform.position += direction * climbSpeed * Time.deltaTime;
            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.Play();
            }
            if (xrRig.transform.position.y >= ladderTop.position.y)
            {
                StartCoroutine(WalkToPlatform());
            }
        }
    }

    IEnumerator WalkToPlatform()
    {
        isClimbing = false;
        cameraLocked = false;

        Vector3 start = xrRig.transform.position;
        Vector3 end = platformTarget.position;

        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / walkDuration;
            xrRig.transform.position = Vector3.Lerp(start, end, t);
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        Transform cameraTransform = Camera.main.transform;
        float cameraYaw = cameraTransform.eulerAngles.y;

        Vector3 directionAwayFromLadder = (platformTarget.position - ladderTop.position).normalized;
        directionAwayFromLadder.y = 0f;

        Quaternion desiredRotation = Quaternion.LookRotation(directionAwayFromLadder, Vector3.up);
        float desiredYaw = desiredRotation.eulerAngles.y; // gpt helped with yaw offset
        float yawOffset = desiredYaw - cameraYaw;

        Vector3 currentEuler = xrRig.transform.eulerAngles;
        Quaternion targetRotation = Quaternion.Euler(0f, currentEuler.y + yawOffset, 0f);

        EnableLocomotion();
        StartCoroutine(SmoothRotate(targetRotation));
    }

    private void DisableLocomotion()
    {
        if (moveProvider != null) moveProvider.enabled = false;
        if (turnProvider != null) turnProvider.enabled = false;
    }

    private void EnableLocomotion()
    {
        if (moveProvider != null) moveProvider.enabled = true;
        if (turnProvider != null) turnProvider.enabled = true;
    }
}