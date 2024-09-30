using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoorController : MonoBehaviour
{
    [SerializeField] private Animator front_door_right = null;
    [SerializeField] private bool openTrigger = false;
    [SerializeField] private bool closeTrigger = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("player"))
        {
            if(openTrigger)
            {
                front_door_right.Play("right_open", 0, 0.0f);
                gameObject.SetActive(false);
            }

            else if (closeTrigger)
            {
                front_door_right.Play("right_close", 0, 0.0f);
                gameObject.SetActive(false);
            }
        }

        
    }
}
