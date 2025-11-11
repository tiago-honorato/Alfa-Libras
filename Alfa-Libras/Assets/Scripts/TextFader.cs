using UnityEngine;
using System.Collections;

public class TextFader : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float fadeDuration = 0.5f;
    public float visibleTime = 0.25f;
    public float waitTime = 2;

    private bool isFading = false;

    public void PlayFade()
    {
        if (!isFading)
            StartCoroutine(FadeInOut());
    }

    public void FadeInOnly()
    {
        if (!isFading)
            StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        isFading = true;
        float elapsed = 0f;

        yield return new WaitForSeconds(waitTime);

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsed / fadeDuration);
            yield return null;
        }
        isFading = false;
    }

    IEnumerator FadeInOut()
    {
        isFading = true;

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsed / fadeDuration);
            yield return null;
        }

        yield return new WaitForSeconds(visibleTime);

        elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(1 - (elapsed / fadeDuration));
            yield return null;
        }

        isFading = false;
    }
}
