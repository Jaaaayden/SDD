using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeCollision : MonoBehaviour
{
    public Rigidbody axeRb;
        
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Tree"))
        {
            axeRb.velocity = Vector3.zero;
            axeRb.isKinematic = true;
        }
    }
}