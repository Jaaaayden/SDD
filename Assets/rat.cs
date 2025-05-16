using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rat : MonoBehaviour

    // Start is called before the first frame update
{
    [Tooltip("The Y threshold below which the object will reset")]
    public float fallThreshold = -5f;

    [Tooltip("Position to reset the object to")]
    public Vector3 resetPosition;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (transform.position.y < fallThreshold)
        {
            ResetObject();
        }
    }

    void ResetObject()
    {
        // Stop any current velocity
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Reset position and rotation
        transform.position = resetPosition;
        transform.rotation = Quaternion.identity;

        Debug.Log("Object reset due to falling.");
    }
}

