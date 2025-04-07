using UnityEngine;
using TMPro; 

public class TerrainCollisionTracker : MonoBehaviour
{
    public TextMeshProUGUI collisionCounterText; 
    private int collidingObjects = 0;
    public AudioClip soundClip; 
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (soundClip != null)
        {
            audioSource.clip = soundClip;
        }
        UpdateCollisionText();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Tree")) 
        {
            collidingObjects++;
            if (soundClip != null && collision.contactCount > 0)
            {
                Vector3 impactPoint = collision.contacts[0].point;
                AudioSource.PlayClipAtPoint(soundClip, impactPoint, 1.5f); // Volume boost if needed
            }
            UpdateCollisionText();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Tree"))
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