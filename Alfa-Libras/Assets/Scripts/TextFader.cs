using UnityEngine;
using System.Collections;

public class TextFader : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float fadeDuration = 0.5f;
    public float visibleTime = 0.25f;

    private bool isFading = false;

    public void PlayFade()
    {
        if (!isFading)
            StartCoroutine(FadeInOut());
    }

    IEnumerator FadeInOut()
    {
        isFading = true;

        // Fade In
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsed / fadeDuration);
            yield return null;
        }

        // Mantém visível por um tempo
        yield return new WaitForSeconds(visibleTime);

        // Fade Out
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
