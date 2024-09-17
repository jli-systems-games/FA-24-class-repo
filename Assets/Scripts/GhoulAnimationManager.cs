using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhoulAnimationManager : MonoBehaviour
{
    Animation ghoul;
    // Start is called before the first frame update
    void Start()
    {
        ghoul = GetComponent<Animation>();
        int clips = ghoul.GetClipCount();

        Debug.Log("clips:" +  clips);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
