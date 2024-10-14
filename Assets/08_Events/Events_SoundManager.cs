using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events_SoundManager : MonoBehaviour
{
    public Events_SimController simController;
    // Start is called before the first frame update
    void Start()
    {
        //simController.onSimDeath += PlayNoise;
        Events_SimController.onSimDeath += PlayNoise;
        //Events_SimController.onSimTakeDamage += 
        //Events_SimController.onSimDeath -= PlayNoise;
    }

    public void PlayNoise()
    {
        Debug.Log("Ah");
    }
}
