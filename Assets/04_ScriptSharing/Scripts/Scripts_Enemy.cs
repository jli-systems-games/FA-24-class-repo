using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scripts_Enemy : MonoBehaviour
{
    //in this example, our enemies don't exist in the scene when we start the game - they spawn in as we go
    //this means I can't just hook up stuff before playing - I have to do it at runtime!

    //I want a reference to the player object - so that I can access the transform of the GameObject whenever I want but I can always grab other components incase I know I want it later
    private GameObject _playerObj;
    
    //and here I know I just need to get the GameManager so I'll just directly reference it
    private Scripts_GameManager _gameManager;

    //Start is called before the first frame update... or when an object is spawned later, when they first spawn
    //I am going to use Awake in this case - there isn't a huge advantage here but it DOES run before start
    //if you are ever wondering the order in which stuff happens and IF it happens, check out here: https://docs.unity3d.com/Manual/ExecutionOrder.html
    void Awake()
    {
        //you can find objects in the scene a couple of ways
        //finding via a tag is great if you want the entire object, and you only have that tag once
        _playerObj = GameObject.FindGameObjectWithTag("Player");

        //finding by type is great if you need a component and that component only exists once in the scene
        _gameManager = FindObjectOfType<Scripts_GameManager>();

        //both of these have versions that let you find multiple things with a tag or of type - returning an array
        //this can be messy if you don't watch out so account for this! 
        //generally speaking, you don't want to architect your scripts in a way that things need to find a reference to multiple instances of the same thing - it will be messy
    }

}
