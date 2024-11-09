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
    public float minPlatformDistance = -1f;
    public float maxPlatformDistance = 2.5f;
    public float minPlatformHeightDifference = -2.0f;
    public float maxPlatformHeightDifference = 2.0f;

    public float baselineHeight = 0.0f; // Set to the initial height of the first platform
    public float minHeight = -9.0f; // Minimum allowed height for platforms
    public float maxHeight = -4.5f; // Maximum allowed height for platforms
    public float heightReturnSpeed = 0.05f; // Adjusts how quickly platforms return to baseline height

    public float minPlatformWidth = 16.5f; // Original width of the platform
    public float widerPlatformChance = 0.5f; // 50% chance for a platform to be wider
    public float minWiderMultiplier = 1.2f; // Minimum multiplier for wider platforms
    public float maxWiderMultiplier = 2f; // Maximum multiplier for wider platforms

    public Obstacle obstacleTemplate;

    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();

        groundCollider = GetComponent<BoxCollider2D>();

        // Set the collider size to always be 16.5 x and 10 y
        groundCollider.size = new Vector2(16.5f, 10.0f);
        screenRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
    }

    void Update()
    {
        groundHeight = transform.position.y + (groundCollider.size.y / 2);
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;
        pos.x -= player.velocity.x * Time.fixedDeltaTime;

        groundRight = transform.position.x + (groundCollider.size.x / 2);

        // Destroy the platform if it moves too far off-screen
        if (groundRight < -20)
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

        // Ensure the collider size is fixed at 16.5 for x and 10 for y
        goCollider.size = new Vector2(16.5f, 10.0f);

        // Set the platform width, occasionally making it wider with random variation
        float adjustedWidth = minPlatformWidth;
        if (Random.value < widerPlatformChance)
        {
            float widerMultiplier = Random.Range(minWiderMultiplier, maxWiderMultiplier);
            adjustedWidth *= widerMultiplier; // Increase width by a random multiplier within the range
        }
        go.transform.localScale = new Vector3(adjustedWidth / minPlatformWidth, go.transform.localScale.y, go.transform.localScale.z);

        // Ensure the collider size remains constant
        goCollider.size = new Vector2(16.5f, 10.0f);

        // Calculate the right edge of the current platform based on its full width
        float currentPlatformRightEdge = transform.position.x + (goCollider.size.x * transform.localScale.x / 2);

        // Set the base minimum spacing and adjust if player's velocity is high
        float minSpacing = 2.0f;
        float speedThreshold = 20.0f; // Adjust this threshold based on your game’s speed
        if (player.velocity.x > speedThreshold)
        {
            minSpacing = 3.5f; // Increase the min gap when the player’s speed is high
        }

        // Calculate the full width of the new platform
        float nextPlatformWidth = adjustedWidth;

        // Add a random factor to vary the spacing between platforms
        float randomSpacing = Random.Range(minPlatformDistance, maxPlatformDistance);

        // Calculate the position of the new platform based on the current platform's right edge and random spacing
        float nextPlatformX = currentPlatformRightEdge + minSpacing + randomSpacing + (nextPlatformWidth / 2);

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
        pos.x = nextPlatformX; // Position based on current platform’s right edge plus minimum spacing and random factor
        pos.y = newHeight;
        go.transform.position = pos;

        // Reset didGenerateGround on the new platform to allow further spawning
        go.GetComponent<Ground>().didGenerateGround = false;

        // Calculate the top of the platform collider for obstacle placement
        float platformTopY = go.transform.position.y + (goCollider.size.y * 0.5f);

        // Minimum spacing between obstacles
        float obstacleSpacing = 2.0f;

        // Store placed obstacle positions to avoid overlap
        List<float> placedObstacleXPositions = new List<float>();

        GroundFall fall = go.GetComponent<GroundFall>();
        if (fall != null)
        {
            Destroy(fall);
            fall = null;
        }

        if (Random.Range(0, 3) == 0)
        {
            fall = go.AddComponent<GroundFall>();
            fall.fallSpeed = Random.Range(0.5f, 2.0f);
        }


        // Randomly determine the number of obstacles to spawn
        int obstacleNum = Random.Range(0, 2);
        for (int i = 0; i < obstacleNum; i++)
        {
            GameObject box = Instantiate(obstacleTemplate.gameObject);

            // Calculate the x position within the platform’s left and right bounds
            float halfWidth = goCollider.size.x / 2 - 1;
            float left = go.transform.position.x - halfWidth;
            float right = go.transform.position.x + halfWidth;

            float x;
            bool validPosition;

            // Attempt to find a valid x position with sufficient spacing from other obstacles
            do
            {
                validPosition = true;
                x = Random.Range(left, right);

                // Check if this x position is too close to any existing obstacles
                foreach (float placedX in placedObstacleXPositions)
                {
                    if (Mathf.Abs(x - placedX) < obstacleSpacing)
                    {
                        validPosition = false;
                        break;
                    }
                }
            } while (!validPosition);

            // Add the valid x position to the list of placed obstacle positions
            placedObstacleXPositions.Add(x);

            // Set the position for the obstacle just above the platform
            Vector2 boxPos = new Vector2(x, platformTopY + 0.1f); // Slight offset above the platform
            box.transform.position = boxPos;

            if (fall != null)
            {
                Obstacle o = box.GetComponent<Obstacle>();
                fall.obstacles.Add(o);
            }
        }
    }
}