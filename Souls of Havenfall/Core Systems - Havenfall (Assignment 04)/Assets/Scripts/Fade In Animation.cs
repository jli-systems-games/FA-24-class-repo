using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FadeInAnimation : MonoBehaviour
{
    public Image fadeImage;               // Reference to the Image component
    public TextMeshProUGUI controlsText;  // Reference to the TextMeshPro text
    public float fadeDuration = 3f;       // Time in seconds for the image fade to complete
    public float textFadeDuration = 5f;   // Time in seconds for the text fade to complete

    private float initialTextAlpha;       // Store the initial alpha value of the text

    void Start()
    {
        // Store the original alpha value from the Inspector to fade to
        initialTextAlpha = controlsText.color.a;

        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        // Set initial image and text colors to their starting values
        Color imageColor = fadeImage.color;
        imageColor.a = 1f; // Start image fully opaque
        fadeImage.color = imageColor;

        Color textColor = controlsText.color;
        textColor.a = 0f;  // Start with text fully transparent
        controlsText.color = textColor;

        // Gradually change the alpha values over their respective durations
        float timer = 0f;
        while (timer < fadeDuration || timer < textFadeDuration)
        {
            timer += Time.deltaTime;

            // Image fades out over fadeDuration
            if (timer < fadeDuration)
            {
                imageColor.a = Mathf.Lerp(1f, 0f, timer / fadeDuration);
                fadeImage.color = imageColor;
            }

            // Text fades in over textFadeDuration
            if (timer < textFadeDuration)
            {
                textColor.a = Mathf.Lerp(0f, initialTextAlpha, timer / textFadeDuration);
                controlsText.color = textColor;
            }

            yield return null;
        }

        // Ensure the image is fully transparent and text is fully visible with the original alpha
        imageColor.a = 0f;
        fadeImage.color = imageColor;

        textColor.a = initialTextAlpha;
        controlsText.color = textColor;

        // Optionally disable the Image component after the fade
        fadeImage.gameObject.SetActive(false);
    }
}
