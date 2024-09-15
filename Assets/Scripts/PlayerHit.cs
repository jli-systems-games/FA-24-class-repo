using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{   
    RaycastHit hit;
    Ray ray;

    public MobMovement mob;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        
        ray= new Ray(transform.position, transform.forward);
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("pressed");
            if(Physics.Raycast(ray, out hit, 5f))
                    {
                        Debug.Log("flashlight");
                        mob.isHunting = false;
            
                    }
        }
        
    }
}
