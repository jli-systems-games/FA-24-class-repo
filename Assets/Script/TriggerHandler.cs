using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class TriggerHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public BoxCollider door;
    public GameManager manage;
    public Animator doorHinge;

    GameState currentState;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") )
        {
            Debug.Log("switching stages");
            if (doorHinge.GetCurrentAnimatorStateInfo(0).IsName("CloseIn"))
            {
                doorHinge.ResetTrigger("CloseIn");
            }
            doorHinge.SetTrigger("openOut");
            manage.AssignState(GameState.Level2);
        }
        else if (other.CompareTag("Enemy"))
        {
            //turn of collider for door and 

            //door.enabled = false;
            doorHinge.SetTrigger("openIn");
        }

       
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("in");
            doorHinge.ResetTrigger("openIn");
            doorHinge.SetTrigger("CloseIn");
        }
    }
}
