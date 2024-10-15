using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Events_SoundManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        Events_SimController.onSimDeath += PlayNoise;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayNoise()
    {
        Debug.Log("This is a noise.");
    }
}
