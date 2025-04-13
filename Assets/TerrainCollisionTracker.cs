using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro; 

public class TerrainCollisionTracker : MonoBehaviour
{
    public TextMeshProUGUI collisionCounterText; 
    private int collidingObjects = 0;
    private HashSet<GameObject> choppedTrees = new HashSet<GameObject>();
    public AudioClip soundClip; 
    public AudioClip soundClip2;
    private AudioSource audioSource;
    public GameObject plankPrefab; 
    public Transform spawnPoint;  

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        UpdateCollisionText();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Tree") && !choppedTrees.Contains(collision.gameObject)) 
        {
            collidingObjects++;
            if (soundClip != null && collision.contactCount > 0)
            {
                Vector3 impactPoint = collision.contacts[0].point;
                AudioSource.PlayClipAtPoint(soundClip, impactPoint, 1.5f);
            }
            UpdateCollisionText();
            StartCoroutine(SpawnWithDelay());
        }

        if (collision.gameObject.CompareTag("Plank")) 
        {
            if (soundClip != null && collision.contactCount > 0)
            {
                Vector3 impactPoint = collision.contacts[0].point;
                AudioSource.PlayClipAtPoint(soundClip2, impactPoint, 1.5f);
            }
        }
    }

    IEnumerator SpawnWithDelay()
    {
        for (int i = 0; i < 3; i++)
        {
            Instantiate(plankPrefab, spawnPoint ? spawnPoint.position : transform.position, Quaternion.identity);
            yield return new WaitForSeconds(1);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Tree") && choppedTrees.Contains(collision.gameObject))
        {
            collidingObjects--;
            UpdateCollisionText();
        }
    }

    private void UpdateCollisionText()
    {
        collisionCounterText.text = "Chopped: " + collidingObjects;
    }
}