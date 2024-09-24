using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scripts_GameManager : MonoBehaviour
{
    //when connecting scripts, you can do it one of two ways
    //the first way is directly referencing the script itself by making a variable of type - name of script:  
    public Scripts_MusicManager musicManager;  //!! making a variable public is great to be able to directly drag scipts whereever needed on the Unity side of things 
                                               //starting out, if you are ever unsure whether to do public or private, I recommend public so that you can always share data and see what is going on on the Unity side of things
    private Scripts_MusicManager _musicManager;
    //the benefit of this is once you have that reference, it takes less typing to get what you need

    //it may also make sense to get a reference to an entire GameObject if you know you may need to access multiple components regularly:
    public GameObject musicManagerObj;
    
    //you should still declare components from that GameObject here though so that you can use it throughout the script
    private Scripts_MusicManager _musicManagerScript; //I'm making them private here because I know I don't plan to share these particular references with other scripts
                                                      //AND I'm going to assign them in the Start() function anyway (see line 32)
    private AudioSource _musicSource;

    //here are a couple of variables that I know other scripts will need access to
    //these NEED to be public so that other scripts can see them!
    public float playerHealth;
    public Transform playerTransform;
    public int enemyAmount;
    //in this example, we are on the GameManager script, so it's good to have "data" variables that I know MULTIPLE other scripts will want to know about
        //ex: so I know the player probably needs to know about the playerHealth, my UI needs to access it to, maybe my enemies need to know about it, and so on...
        //having it here makes it easy to add things to find it, because it is always in a central location


    // Start is called before the first frame update
    void Start()
    {
        //if you are looking for another component on the gameObject THIS script is on, you use this:
        _musicManagerScript = GetComponent<Scripts_MusicManager>();

        //if you are looking for a component on ANOTHER GameObject, use this:
        _musicSource = musicManagerObj.GetComponent<AudioSource>(); //notice I state which GameObject I'm looking for the component on!
    }

}
