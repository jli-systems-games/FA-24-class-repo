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

    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            StartCoroutine(snowballTriggerDetection());
            animator.SetTrigger("throw");
        }
    }
}

