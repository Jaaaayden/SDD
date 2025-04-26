using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class TreeChopping : MonoBehaviour
{
    public Rigidbody upperTreeRb;
    public int maxHits = 5; 
    private int currentHits = 0;
    public AudioClip soundClip;     
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (soundClip != null)
        {
            audioSource.clip = soundClip;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Axe")) 
        {
            currentHits++;
            AudioSource.PlayClipAtPoint(soundClip, transform.position);
            // Debug.Log("Tree hit! " + currentHits + "/" + maxHits);

            if (currentHits >= maxHits) 
            {
                FallDown();
            }
        }
    }

    private void FallDown()
    {
        Debug.Log("Tree falls!");
        upperTreeRb.isKinematic = false;
        upperTreeRb.useGravity = true;

        StartCoroutine(ApplyForceAfterPhysicsUpdate());
    }

    IEnumerator ApplyForceAfterPhysicsUpdate()
    {
        yield return new WaitForFixedUpdate(); 

        Vector3 pushDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));

        upperTreeRb.AddForce(pushDirection * 100f, ForceMode.Impulse);
        upperTreeRb.AddTorque(Vector3.forward * 200f, ForceMode.Impulse);
    }
}