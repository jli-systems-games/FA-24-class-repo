using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogController : MonoBehaviour
{
    public float transitionSpeed = 2.0f; // Speed for transitioning fog density
    private Coroutine fogTransitionCoroutine; // Reference to the ongoing transition

    private void Start()
    {
        // Initialize the fog with a starting density
        RenderSettings.fog = true; // Ensure fog is enabled
        RenderSettings.fogDensity = 0.08f; // Set initial density
    }

    // Call this method to change the fog density to a target value
    public void ChangeFogDensity(float targetDensity)
    {
        Debug.Log($"Requested fog density change to: {targetDensity}"); // Log the target density

        // If there's an ongoing transition, stop it
        if (fogTransitionCoroutine != null)
        {
            StopCoroutine(fogTransitionCoroutine);
        }

        // Start a new fog density transition
        fogTransitionCoroutine = StartCoroutine(ChangeFogDensityOverTime(targetDensity));
    }

    // Coroutine to handle the gradual change of fog density
    private IEnumerator ChangeFogDensityOverTime(float targetDensity)
    {
        Debug.Log($"Starting fog density transition to: {targetDensity}"); // Log when the transition starts

        float startDensity = RenderSettings.fogDensity; // Get the current fog density
        float elapsed = 0.0f; // Track the elapsed time

        // Gradually change the fog density over time
        while (elapsed < transitionSpeed)
        {
            RenderSettings.fogDensity = Mathf.Lerp(startDensity, targetDensity, elapsed / transitionSpeed);
            Debug.Log($"Current Fog Density: {RenderSettings.fogDensity}"); // Log the current fog density
            elapsed += Time.deltaTime; // Increment elapsed time
            yield return null; // Wait for the next frame
        }

        // Ensure the final fog density is set to the target value
        RenderSettings.fogDensity = targetDensity;
        Debug.Log($"Final Fog Density set to: {RenderSettings.fogDensity}"); // Log the final density
    }
}
