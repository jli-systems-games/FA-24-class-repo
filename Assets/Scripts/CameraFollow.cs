using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float offsetX = 9.05f; // Horizontal offset to keep the player on the left side
    public float offsetY = 0.75f; // Fixed vertical offset to keep the player lower on the screen

    void Update()
    {
        // Ensure the camera follows the player's horizontal movement only
        if (player != null)
        {
            transform.position = new Vector3(player.position.x + offsetX, offsetY, transform.position.z);
        }
    }
}
