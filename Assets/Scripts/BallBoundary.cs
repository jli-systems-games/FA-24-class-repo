using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BallBoundary
{
    // Method to check if the player is out of bounds
    public static bool IsOutOfBounds(GameObject player, float negativeBoundaryX, float positiveBoundaryX)
    {
        // Check if the GameObject's tag is "Player" and if it crosses the X boundaries
        if (player.CompareTag("Player") &&
            (player.transform.position.x < negativeBoundaryX || player.transform.position.x > positiveBoundaryX))
        {
            return true;
        }
        return false;
    }
}
