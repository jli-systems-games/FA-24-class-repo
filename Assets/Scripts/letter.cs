using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class letter : MonoBehaviour
{
    public GameObject letterUI;
    bool toggle;
    public PlayerCamera player;

    public void openCloseLetter()
    {
        toggle = !toggle;
        if(toggle == false)
        {
            letterUI.SetActive(false);
            player.enabled = true;

        }
        if(toggle == true)
        {
            letterUI.SetActive(true);
            player.enabled = false;
        }
    }
}
