using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightSwitch : MonoBehaviour
{
    public Image darkOverlay;
    public Image lightSwitch;
    public Sprite switchOn;
    public Sprite switchOff;

    public AudioClip switchOnSound;
    public AudioClip switchOffSound;
    private AudioSource audioSource;

    public float fadeDuration = 0.2f;

    private bool isLightOn = true;
    private float targetAlpha;

    // Start is called before the first frame update
    void Start()
    {
        // Set the initial alpha for the overlay
        float initialAlpha = isLightOn ? 0f : 0.7f;
        darkOverlay.color = new Color(0, 0, 0, initialAlpha);

        // Set the correct switch sprite
        lightSwitch.sprite = isLightOn ? switchOn : switchOff;

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ToggleLight()
    {
        isLightOn = !isLightOn;

        // Set target alpha for the overlay
        targetAlpha = isLightOn ? 0f : 0.7f;
        StartCoroutine(FadeOverlay());

        PlaySound(isLightOn ? switchOnSound : switchOffSound);
    }

    // Coroutine to fade the dark overlay and update the sprite simultaneously
    private IEnumerator FadeOverlay()
    {
        float startAlpha = darkOverlay.color.a;
        float elapsedTime = 0f;

        // Change the sprite immediately
        lightSwitch.sprite = isLightOn ? switchOn : switchOff;

        // Fade the overlay gradually
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            darkOverlay.color = new Color(0, 0, 0, newAlpha);  // Set the new alpha
            yield return null;
        }

        // Ensure the final alpha is set
        darkOverlay.color = new Color(0, 0, 0, targetAlpha);
    }

    private void PlaySound(AudioClip clip)
    { 
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}