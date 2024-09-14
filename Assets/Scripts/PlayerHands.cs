using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHands : MonoBehaviour
{
    public SpriteRenderer spriteRenderer; 
    public float delay = 0.5f; 
    public float fadeDuration = 0.5f; 

    private void Start()
    {
        StartCoroutine(FadeOutAndDestroy());
    }

    private IEnumerator FadeOutAndDestroy()
    {
        yield return new WaitForSeconds(delay);

        Color startColor = spriteRenderer.color;
        startColor.a = 1f; 

        Color endColor = startColor;
        endColor.a = 0f;

        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / fadeDuration;

            spriteRenderer.color = Color.Lerp(startColor, endColor, t);

            yield return null;
        }

        spriteRenderer.color = endColor;

        Destroy(gameObject);
    }
}
