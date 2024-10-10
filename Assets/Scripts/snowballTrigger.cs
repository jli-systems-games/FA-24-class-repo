using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snowballTrigger : MonoBehaviour
{
    public Animator animator;

    private IEnumerator snowballTriggerDetection()
    {
        GameObject varGameObject = GameObject.FindWithTag("Player");
		varGameObject.GetComponent<Player>().enabled = false;
        yield return new WaitForSeconds(5f);
        varGameObject.GetComponent<Player>().enabled = true;
    }

    private IEnumerator snowballAim()
    {
        yield return new WaitForSeconds(2f);
        animator.speed = 0;
    }

    public void snowballRelease()
    {
        Debug.Log("called");
        animator.speed = 1;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            StartCoroutine(snowballTriggerDetection());
            animator.SetTrigger("throw");
        }

        if (Input.GetMouseButton(0))
        {
            StartCoroutine(snowballAim());
        }

        if(Input.GetMouseButtonUp(0))
        {
            snowballRelease();
        }
    }
}

