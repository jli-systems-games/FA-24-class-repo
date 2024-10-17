using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInManager : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1.5f; // Duration of the fade-in

    private void Start()
    {
        // Start the fade-in process
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        // Sets the initial alpha value to 1 (fully opaque)
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 1);

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;

            // Smoothly interpolate the alpha value
            float t = Mathf.Clamp01(elapsedTime / fadeDuration);
            float alpha = Mathf.Lerp(1, 0, t);

            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, alpha);
            yield return null; // Wait for the next frame
        }

        // Ensures the image is fully transparent at the end
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 0);

        // Destroys the fade image to allow interaction with UI
        Destroy(fadeImage.gameObject);
    }
}
