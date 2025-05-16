using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;
using System.Collections;

public class lastt : MonoBehaviour
{
    public ParticleSystem particleEffect;
    public string sceneToLoad = "ggha"; // Change this to your actual scene name

    private void Start()
    {
        if (particleEffect != null)
            particleEffect.Stop();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the other object has an XRGrabInteractable
        XRGrabInteractable otherGrab = collision.gameObject.GetComponent<XRGrabInteractable>();
        XRGrabInteractable thisGrab = GetComponent<XRGrabInteractable>();

        if (otherGrab != null && thisGrab != null)
        {
            // Start delayed particle effect
            Vector3 contactPoint = collision.contacts[0].point;
            StartCoroutine(PlayEffectAfterDelay(contactPoint, 3f));
        }
    }

    private IEnumerator PlayEffectAfterDelay(Vector3 position, float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);

        if (particleEffect != null)
        {
            particleEffect.transform.position = position;
            particleEffect.Play();
            StartCoroutine(ChangeSceneAfterDelay(5f)); // Scene changes 5 seconds after particles start
        }
    }

    private IEnumerator ChangeSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneToLoad);
    }
}
