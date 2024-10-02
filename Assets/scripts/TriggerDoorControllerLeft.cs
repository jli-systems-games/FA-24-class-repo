using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoorControllerLeft : MonoBehaviour
{
    [SerializeField] private Animator front_door_left = null;
    [SerializeField] private bool openTrigger = false;
    [SerializeField] private bool closeTrigger = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (openTrigger)
            {
                front_door_left.Play("left_open", 0, 0.0f);
                gameObject.SetActive(false);
            }

            else if (closeTrigger)
            {
                //front_door_left.Play("right_close", 0, 0.0f);
                gameObject.SetActive(false);
            }
        }


    }
}
