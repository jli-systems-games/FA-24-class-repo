using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    Player player;

    public float groundHeight;
    public float groundRight;
    public float screenRight;
    BoxCollider2D groundCollider;

    bool didGenerateGround = false;

    // Adjust these to control the platform generation based on player velocity
    public float minPlatformDistance = 1.5f;
    public float maxPlatformDistance = 5.0f;
    public float minPlatformHeightDifference = -2.0f;
    public float maxPlatformHeightDifference = 2.0f;

    public float baselineHeight = 0.0f; // Set to the initial height of the first platform
    public float minHeight = -9.0f; // Minimum allowed height for platforms
    public float maxHeight = -4.5f; // Maximum allowed height for platforms
    public float heightReturnSpeed = 0.05f; // Adjusts how quickly platforms return to baseline height

    public float minPlatformWidth = 16.5f; // Original width of the platform
    public float widerPlatformChance = 0.5f; // 50% chance for a platform to be wider
    public float minWiderMultiplier = 1.5f; // Minimum multiplier for wider platforms
    public float maxWiderMultiplier = 3f; // Maximum multiplier for wider platforms


    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();

        groundCollider = GetComponent<BoxCollider2D>();
        groundHeight = transform.position.y + (groundCollider.size.y / 2);
        screenRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;
        pos.x -= player.velocity.x * Time.fixedDeltaTime;

        groundRight = transform.position.x + (groundCollider.size.x / 2);

        // Destroy the platform if it moves too far off-screen
        if (groundRight < -10)
        {
            Destroy(gameObject);
            return;
        }

        // Generate a new platform if the current one reaches the screen’s right edge
        if (!didGenerateGround && groundRight < screenRight)
        {
            didGenerateGround = true;
            generateGround();
        }

        transform.position = pos;
    }

    void generateGround()
    {
        // Create a new platform
        GameObject go = Instantiate(gameObject);
        BoxCollider2D goCollider = go.GetComponent<BoxCollider2D>();

        // Set the platform width, occasionally making it wider with random variation
        float adjustedWidth = minPlatformWidth;
        if (Random.value < widerPlatformChance)
        {
            float widerMultiplier = Random.Range(minWiderMultiplier, maxWiderMultiplier);
            adjustedWidth *= widerMultiplier; // Increase width by a random multiplier within the range
        }
        go.transform.localScale = new Vector3(adjustedWidth / minPlatformWidth, go.transform.localScale.y, go.transform.localScale.z);

        // Update collider size to match the new width
        goCollider.size = new Vector2(adjustedWidth, goCollider.size.y);

        // Define a random horizontal distance between platforms within min and max range
        float randomDistance = Random.Range(minPlatformDistance, maxPlatformDistance);

        // Calculate the right edge of the current platform
        float currentPlatformRightEdge = groundRight + (goCollider.size.x / 2);

        // Calculate the max jump reach to limit vertical platform position based on jump velocity
        float playerJumpReach = player.jumpVelocity * player.jumpVelocity / (2 * Physics2D.gravity.magnitude);
        float heightVariation = Mathf.Clamp(Random.Range(minPlatformHeightDifference, maxPlatformHeightDifference), -playerJumpReach, playerJumpReach);

        // Gradually return platforms closer to the baseline height and ensure they don’t go above maxHeight or below minHeight
        float adjustedHeightDifference = Mathf.Lerp(heightVariation, baselineHeight - transform.position.y, heightReturnSpeed);
        float newHeight = transform.position.y + adjustedHeightDifference;

        // Clamp the new height to stay within minHeight and maxHeight
        newHeight = Mathf.Clamp(newHeight, minHeight, maxHeight);

        // Set up the position for the new platform relative to the current platform's right edge
        Vector2 pos;
        pos.x = currentPlatformRightEdge + randomDistance; // Dynamic spacing based on min and max range
        pos.y = newHeight;
        go.transform.position = pos;

        // Reset didGenerateGround on the new platform to allow further spawning
        go.GetComponent<Ground>().didGenerateGround = false;
    }
}
