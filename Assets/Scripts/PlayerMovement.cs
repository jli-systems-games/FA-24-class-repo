using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float minY = -5f;
    public float maxY = 5f;

    private EncounterManager encounterManager;

    void Start()
    {
        encounterManager = FindObjectOfType<EncounterManager>();
    }

    void Update()
    {
        // Ensure the player can only move during the Idle state
        if (encounterManager != null && encounterManager.currentState == EncounterManager.EncounterState.Idle)
        {
            MovePlayer();
        }
    }

    void MovePlayer()
    {
        // Get input from both WASD keys and Arrow keys
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        transform.Translate(movement * moveSpeed * Time.deltaTime);

        // Clamp the object's y position between minY and maxY
        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);

        // Update the object's position with the clamped y value
        transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);
    }
}
