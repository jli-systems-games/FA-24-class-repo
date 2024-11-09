using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float depth = 1;  // Determines the parallax effect strength; higher values move slower

    Player player; // Reference to the Player script to access player velocity

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        // Calculate the parallax effect by adjusting the player’s X velocity based on depth
        float realVelocity = player.velocity.x / depth;

        // Get the current position of the parallax object
        Vector2 pos = transform.position;

        // Move the background position to the left based on the calculated velocity
        pos.x -= realVelocity * Time.fixedDeltaTime;

        // Loop the background position if it moves too far left, creating a seamless effect
        if (pos.x <= -15)
            pos.x = 15;

        // Update the position of the object to create the parallax effect
        transform.position = pos;
    }
}
