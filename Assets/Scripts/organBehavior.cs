using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class organBehavior : MonoBehaviour
{
    public GameManager manager;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.parent.name != "Fridge")
        {
            if(manager.current == GameState.summer)
            {   
                if(transform.parent.tag != "jar")
                {
                    manager.ChangeState(GameState.rotting);

                }
                else
                {
                    Debug.Log("youareFine");
                }
                

            }
        }
    }
}
