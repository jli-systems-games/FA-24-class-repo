using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    Player player;  // Reference to the Player script, used to access player data (e.g., distance)
    TextMeshProUGUI distanceText;  // Reference to the TMP UI element that displays distance

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        distanceText = GameObject.Find("Distance Text").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        // Retrieves and converts player's distance to an integer, then displays it in the UI
        int distance = Mathf.FloorToInt(player.distance);  // Converts distance to whole number
        distanceText.text = distance + " m";  // Updates distance text in meters
    }
}
