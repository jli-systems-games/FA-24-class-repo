using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen: MonoBehaviour
{
    public Animator animator; 
    public GameObject animationObject; 
    public GameObject before;
    public GameObject after;

    private bool inTriggerZone = false;
    private bool animationPlayed = false;

    private void Start()
    {
       

        animator.SetBool("IsRotating", false); 
        animationObject.SetActive(false);
        before.SetActive(true);
        after.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inTriggerZone = true;
            
            if (!animationPlayed)
            {
                animator.SetBool("IsRotating", true);
                animationObject.SetActive(true);
                before.SetActive(false);
                StartCoroutine(OpenAfterObjectAfterDelay(5.0f)); 
                animationPlayed = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inTriggerZone = false;
        }
    }

    IEnumerator OpenAfterObjectAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        after.SetActive(true);
        animationObject.SetActive(false);
    }
}