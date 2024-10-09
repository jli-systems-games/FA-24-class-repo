using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSeason : MonoBehaviour
{
    //Winter and SUmmer objects

    [SerializeField] GameObject knob, WinterSet, SummerSet;
    [SerializeField] PickUpNRotate pick;
    [SerializeField] GameManager manage;
    Animator anim;

    bool animateDone = false;
    void Start()
    {
        anim = knob.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void seasonSwitch(float turndirection)
    {
        //Debug.Log("turndirct" + turndirection);
        if(turndirection < 0) 
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("right"))
            {
                anim.ResetTrigger("StartRight");
                
            }
            anim.SetTrigger("StartLeft");
            StartCoroutine(timingAnimation(turndirection));
        }
        else if (turndirection > 0)
        {
            //Debug.Log("turning right");
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("left"))
            {
                anim.ResetTrigger("StartLeft");
            }
            anim.SetTrigger("StartRight");
            StartCoroutine(timingAnimation(turndirection));
        }
    }

    IEnumerator timingAnimation(float direction)
    {
        while(anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !anim.IsInTransition(0))
        {
            Debug.Log("animation playing");
            yield return null;
        }
        animateDone = true;
        pick.DisableKnob();
        if (direction > 0)
        {
            WinterSet.SetActive(true);
            SummerSet.SetActive(false);
            manage.ChangeState(GameState.winter);

        }else if (direction < 0)
        {
            WinterSet.SetActive(false);
            SummerSet.SetActive(true);
            manage.ChangeState(GameState.summer);
        }
        yield break;

    }
}
