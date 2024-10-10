using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogZoneTrigger : MonoBehaviour
{
    public float targetFogDensity; // The target fog density for this zone
    private FogController fogController; // Reference to the FogController

    private void Start()
    {
        fogController = FindObjectOfType<FogController>(); // Find the FogController in the scene
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure the colliding object is the player
        {
            Debug.Log($"Entered fog zone with target density: {targetFogDensity}"); // Log entry
            fogController.ChangeFogDensity(targetFogDensity); // Directly change to this zone's fog density
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure the exiting object is the player
        {
            Debug.Log($"Exited fog zone with target density: {targetFogDensity}"); // Log exit
            // Optionally, you can handle exit here if needed, but usually, you would want to change fog density back to the previous zone.
        }
    }
}
