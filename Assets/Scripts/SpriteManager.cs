using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpriteManager : MonoBehaviour
{
    [System.Serializable]
    public class SpriteGroup
    {
        public List<SpriteRenderer> sprites; // Sprites in this group
    }

    public List<SpriteGroup> spriteGroups; // List of sprite groups
    public float fadeDuration = 1f;        // Duration for fade-in and fade-out effects
    public KeyCode triggerKey = KeyCode.Space; // Key to trigger the next group

    private int currentGroupIndex = 0;     // Tracks the current group

    private void Update()
    {
        // Check for the keypress
        if (Input.GetKeyDown(triggerKey) && currentGroupIndex < spriteGroups.Count)
        {
            // Start the transition to the next group
            StartCoroutine(TransitionGroups(currentGroupIndex, currentGroupIndex + 1));
            currentGroupIndex++;
        }
    }

    private IEnumerator TransitionGroups(int currentIndex, int nextIndex)
    {
        // Fade out the current group
        if (currentIndex < spriteGroups.Count)
        {
            StartCoroutine(FadeOutGroup(spriteGroups[currentIndex].sprites));
        }

        // Fade in the next group
        if (nextIndex < spriteGroups.Count)
        {
            yield return StartCoroutine(FadeInGroup(spriteGroups[nextIndex].sprites));
        }
    }

    private IEnumerator FadeInGroup(List<SpriteRenderer> group)
    {
        float elapsedTime = 0f;

        // Cache the original alpha values
        List<Color> startColors = new List<Color>();
        foreach (var sprite in group)
        {
            startColors.Add(sprite.color);
        }

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;

            // Update alpha for each sprite in the group
            for (int i = 0; i < group.Count; i++)
            {
                var sprite = group[i];
                var color = sprite.color;
                color.a = Mathf.Lerp(startColors[i].a, 1f, elapsedTime / fadeDuration);
                sprite.color = color;
            }

            yield return null; // Wait for the next frame
        }

        // Ensure all sprites in the group are fully opaque
        foreach (var sprite in group)
        {
            var color = sprite.color;
            color.a = 1f;
            sprite.color = color;
        }
    }

    private IEnumerator FadeOutGroup(List<SpriteRenderer> group)
    {
        float elapsedTime = 0f;

        // Cache the original alpha values
        List<Color> startColors = new List<Color>();
        foreach (var sprite in group)
        {
            startColors.Add(sprite.color);
        }

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;

            // Update alpha for each sprite in the group
            for (int i = 0; i < group.Count; i++)
            {
                var sprite = group[i];
                var color = sprite.color;
                color.a = Mathf.Lerp(startColors[i].a, 0f, elapsedTime / fadeDuration);
                sprite.color = color;
            }

            yield return null; // Wait for the next frame
        }

        // Ensure all sprites in the group are fully transparent
        foreach (var sprite in group)
        {
            var color = sprite.color;
            color.a = 0f;
            sprite.color = color;
        }
    }
}
