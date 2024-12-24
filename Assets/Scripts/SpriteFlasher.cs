using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpriteFlasher : MonoBehaviour
{
    [System.Serializable]
    public class FadeableSprite
    {
        public SpriteRenderer spriteRenderer; // Sprite to fade
    }

    public List<FadeableSprite> spritesToFade; // List of sprites to fade
    public float fadeDuration = 1f;           // Duration for one fade-in or fade-out
    public float maxRandomStartDelay = 2f;    // Maximum random delay before a sprite starts fading
    public bool loop = true;                  // Whether the fading effect should loop

    private void Start()
    {
        // Start fading for each sprite with a random delay
        foreach (var sprite in spritesToFade)
        {
            float randomDelay = Random.Range(0f, maxRandomStartDelay);
            StartCoroutine(FadeAlphaWithDelay(sprite.spriteRenderer, randomDelay));
        }
    }

    private IEnumerator FadeAlphaWithDelay(SpriteRenderer spriteRenderer, float delay)
    {
        // Wait for the random delay
        yield return new WaitForSeconds(delay);

        // Start the fade-in and fade-out loop
        do
        {
            // Fade out
            yield return StartCoroutine(FadeAlpha(spriteRenderer, 1f, 0f));

            // Fade in
            yield return StartCoroutine(FadeAlpha(spriteRenderer, 0f, 1f));
        } while (loop);
    }

    private IEnumerator FadeAlpha(SpriteRenderer spriteRenderer, float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;
        Color color = spriteRenderer.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            color.a = newAlpha;
            spriteRenderer.color = color;
            yield return null; // Wait for the next frame
        }

        // Ensure the alpha is exactly at the target value
        color.a = endAlpha;
        spriteRenderer.color = color;
    }
}
