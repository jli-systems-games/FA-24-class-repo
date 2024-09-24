using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scripts_Player : MonoBehaviour
{
    //here, I'm demonstrating how to assign variables in Start() - this is great for things that may spawn in after the scene starts...
    //but it's also great to do even if you've made them public so you don't end up with "null" variables
    private Scripts_MusicManager _musicManager;
    private Scripts_GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        //Scripts_Enemy has some more information on the script, but once again:
        //finding objects of type is ideal for grabbing a particular component, and finding via tag is great for entire objects
        //that said, you can still get to a component from a tag search, like here:
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Scripts_GameManager>(); //find the object, then get component

        //here, I've declared a variable in a function, which means I can't use tempGM outside of Start(), it's great for... temp variables
        GameObject tempGM = GameObject.FindGameObjectWithTag("GameManager"); 
        //this can be useful if you need something for local math, or just need the GameObject to get to the components 
    }

}
