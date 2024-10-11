using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartNEndScreenManager : MonoBehaviour
{
    public Image thankYouSprite;      // "Thank you for playing..." sprite
    public Image titleScreenSprite;   // Title sprite for the start screen
    public float fadeDuration = 1f;   // Fade duration
    public float delayBeforeTitle = 2f; // Delay before fading in the title

    // Public references to player and camera
    public GameObject player;         // Player object
    public GameObject cam;            // Camera object

    private PlayerMovement playerMovement; // Player movement script reference
    private PlayerCamera cameraController; // Player camera script reference

    void Start()
    {
        // Get references to the player and camera movement scripts
        playerMovement = player.GetComponent<PlayerMovement>();
        cameraController = cam.GetComponent<PlayerCamera>();

        // Start the start screen sequence
        StartCoroutine(StartScreenSequence());
    }

    IEnumerator StartScreenSequence()
    {
        // Disable player and camera movement
        playerMovement.enabled = false;
        cameraController.enabled = false;

        // Set both sprites to fully transparent initially
        thankYouSprite.color = new Color(1, 1, 1, 0);
        titleScreenSprite.color = new Color(1, 1, 1, 0);

        // Activate the "Thank you" sprite
        thankYouSprite.gameObject.SetActive(true);

        // Fade in the "Thank you" sprite
        yield return StartCoroutine(FadeIn(thankYouSprite, fadeDuration));

        // Wait for a moment to display the "Thank you" message
        yield return new WaitForSeconds(delayBeforeTitle);

        // Activate the title sprite
        titleScreenSprite.gameObject.SetActive(true);

        // Fade in the title screen sprite
        yield return StartCoroutine(FadeIn(titleScreenSprite, fadeDuration));

        // Wait for a short moment before fading both out
        yield return new WaitForSeconds(2f);

        // Fade both sprites out together
        yield return StartCoroutine(FadeOut(thankYouSprite, fadeDuration));
        yield return StartCoroutine(FadeOut(titleScreenSprite, fadeDuration));

        // Re-enable player and camera movement after both sprites fade out
        playerMovement.enabled = true;
        cameraController.enabled = true;

        // Optionally, you can load the next scene or perform another action here
        // e.g., LoadNextScene();
    }

    IEnumerator FadeIn(Image image, float duration)
    {
        float elapsedTime = 0f;
        Color color = image.color;

        while (elapsedTime < duration)
        {
            color.a = Mathf.Lerp(0, 1, elapsedTime / duration);
            image.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure it's fully opaque
        color.a = 1;
        image.color = color;
    }

    IEnumerator FadeOut(Image image, float duration)
    {
        float elapsedTime = 0f;
        Color color = image.color;

        while (elapsedTime < duration)
        {
            color.a = Mathf.Lerp(1, 0, elapsedTime / duration);
            image.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure it's fully transparent
        color.a = 0;
        image.color = color;
        image.gameObject.SetActive(false); // Deactivate the sprite after fading out
    }
}
