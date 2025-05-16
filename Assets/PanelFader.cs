using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CanvasGroup))]
public class PanelFader : MonoBehaviour
{
    [Header("Fade Settings")]
    public float fadeDuration = 1.0f;
    public float initialDelay = 5.0f;

    [Header("Optional Settings")]
    [Tooltip("If true, the panel will start hidden and skip fade-in.")]
    public bool hasSeenBefore = false;

    private CanvasGroup canvasGroup;
    private Coroutine fadeCoroutine;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        if (hasSeenBefore)
        {
            canvasGroup.alpha = 0f;
            gameObject.SetActive(false);
        }
        else
        {
            canvasGroup.alpha = 1f;
        }
    }

    void Start()
    {
        if (!hasSeenBefore)
        {
            StartCoroutine(FadeOutAfterDelay(initialDelay));
            hasSeenBefore = true;
        }
    }

    IEnumerator FadeOutAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        FadeOut();
    }

    public void FadeOut()
    {
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeTo(0f));
    }

    public void FadeIn()
    {
        gameObject.SetActive(true);
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeTo(1f));
    }

    IEnumerator FadeTo(float targetAlpha)
    {
        float startAlpha = canvasGroup.alpha;
        float timer = 0f;

        Debug.Log($"{gameObject.name} fading to {targetAlpha} over {fadeDuration} seconds");

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float progress = Mathf.Clamp01(timer / fadeDuration);
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, progress);
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;

        if (Mathf.Approximately(targetAlpha, 0f))
        {
            gameObject.SetActive(false);
        }
    }
}
