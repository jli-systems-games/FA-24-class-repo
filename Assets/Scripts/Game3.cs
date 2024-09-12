using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Game3 : MonoBehaviour
{
    public TMP_Text pressCountText;   // Text to display how many presses are needed

    private int targetPresses;
    private int currentPresses;

    // Start is called before the first frame update
    void Start()
    {
        targetPresses = Random.Range(10, 31);
        currentPresses = 0;
        pressCountText.text = targetPresses.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentPresses++;
        }

        // If the player presses space the exact number of times
        if (currentPresses == targetPresses)
        {
            GameManager.instance.OnGameComplete(true);
        }
    }
}
