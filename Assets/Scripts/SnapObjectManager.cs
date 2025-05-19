using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;
using TMPro;

public class SnapObjectManager : MonoBehaviour
{
    [Header("Sound Effects")]

    public AudioSource audioSource;
    public AudioClip connectSFX;
    public AudioClip disconnectSFX;

    [Header("Debug")]

    public TextMeshPro debugTextbox;

    private XRGrabInteractable grabInteractable;
    private Rigidbody rb;
    private List<Joint> activeJoints = new List<Joint>();
    

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        grabInteractable = GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();
        grabInteractable.selectEntered.AddListener(OnGrabbed);
        grabInteractable.selectExited.AddListener(OnReleased);
    }

    public void CreateJoint(Rigidbody otherRB, float snapForce){
        Rigidbody thisRB = GetComponent<Rigidbody>();
        if (thisRB == otherRB) return;

        FixedJoint joint = thisRB.gameObject.AddComponent<FixedJoint>();
        joint.connectedBody = otherRB;
        //joint.breakForce = snapForce;

        audioSource.PlayOneShot(connectSFX);
        activeJoints.Add(joint);
        debugTextbox.text = "snapped";
    }  

    void OnJointBreak(float breakForce)
    {
        debugTextbox.text = ($"Joint broke with force: {breakForce}"); // Console verification
        for (int i = activeJoints.Count - 1; i >= 0; i--)
        {
            if (activeJoints[i] == null) // Joint was broken or destroyed
            {
                activeJoints.RemoveAt(i);
            }
        }

        //Play sound effect
        audioSource.PlayOneShot(disconnectSFX);

    }    

    void OnGrabbed(SelectEnterEventArgs args)
    {
        // Unfreeze all connected rigidbodies
        foreach (Rigidbody connectedRB in GetAllConnectedRigidbodies())
        {
            connectedRB.constraints = RigidbodyConstraints.None;
        }
    }

    void OnReleased(SelectExitEventArgs args)
    {
        // Freeze all connected rigidbodies
        foreach (Rigidbody connectedRB in GetAllConnectedRigidbodies())
        {
            connectedRB.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    List<Rigidbody> GetAllConnectedRigidbodies()
    {
        List<Rigidbody> connected = new List<Rigidbody>();
        HashSet<Rigidbody> visited = new HashSet<Rigidbody>();
        Queue<Rigidbody> queue = new Queue<Rigidbody>();
        queue.Enqueue(rb);
        visited.Add(rb);

        while (queue.Count > 0)
        {
            Rigidbody current = queue.Dequeue();
            connected.Add(current);

            foreach (Joint joint in current.GetComponents<Joint>())
            {
                Rigidbody connectedBody = joint.connectedBody;
                if (connectedBody != null && !visited.Contains(connectedBody))
                {
                    visited.Add(connectedBody);
                    queue.Enqueue(connectedBody);
                }
            }
        }
        return connected;
    }
}