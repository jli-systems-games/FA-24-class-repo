using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum AnimationStages
{
        Hunting, Caught, Jumpscare
}
public class GhoulAnimationManager : MonoBehaviour
{
    public AnimationStages stage;
  
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

    public void ChangeStage(AnimationStages newStage)
    {
        stage = newStage;

        if(stage == AnimationStages.Hunting)
        {
            //play run
            ghoul.Play("Run");
            ghoul.wrapMode = WrapMode.Loop;
        }
        else if(stage == AnimationStages.Caught)
        {
            //crossFade to death
            ghoul.Play("Death");
            ghoul.wrapMode = WrapMode.ClampForever;
        }else if(stage==AnimationStages.Jumpscare)
        {
            //crossFade to Jumpscare
            ghoul.Play("Attack2");
            ghoul.wrapMode = WrapMode.ClampForever;
        }
    }
}
