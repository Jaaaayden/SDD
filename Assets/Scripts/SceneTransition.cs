using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip helicopterAudio;
    public Transform xrRig;
    public float delayBeforeLoad = 5f;

    private bool hasTriggered = false;
    private bool audioPlayed = false; 

    private void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true;
            StartCoroutine(Transition());
        }
    }

    private System.Collections.IEnumerator Transition()
    {
        if (!audioPlayed && helicopterAudio != null)
        {
            AudioSource.PlayClipAtPoint(helicopterAudio, xrRig.position);
            audioPlayed = true;
        }

        yield return new WaitForSeconds(delayBeforeLoad);

        SceneManager.LoadScene(2);
    }
}
