using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerButtonAnimController : MonoBehaviour
{
    private  Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TriggerButtonAnim()
    {
        animator.SetTrigger("Trigger");    
    }
}
