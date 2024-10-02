using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoorControllerRoom1 : MonoBehaviour
{
    [SerializeField] private Animator pivit_room_1 = null;
    [SerializeField] private bool openTrigger = false;
    [SerializeField] private bool closeTrigger = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (openTrigger)
            {
                pivit_room_1.Play("room1_open", 0, 0.0f);
                gameObject.SetActive(false);
            }

            else if (closeTrigger)
            {
                //pivit_room_1.Play("right_close", 0, 0.0f);
                gameObject.SetActive(false);
            }
        }


    }
}
