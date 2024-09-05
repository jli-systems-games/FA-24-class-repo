using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paddleControllers : MonoBehaviour
{
    public float speed = 0f;


    void Update()
    {
        if ( gameObject.tag == "player1" )
        {
            if (Input.GetKey(KeyCode.X)) {
            transform.position += new Vector3(0, 0, speed) * Time.deltaTime;
			
		    }
		
            if (Input.GetKey(KeyCode.Z)) {
            transform.position += new Vector3(0, 0, -speed) * Time.deltaTime;
			
		    }
        }

        if ( gameObject.tag == "player2" )
        {

            if (Input.GetKey(KeyCode.N)) {
                transform.position += new Vector3(0, 0, speed) * Time.deltaTime;
                
            }
            
            if (Input.GetKey(KeyCode.B)) {
                transform.position += new Vector3(0, 0, -speed) * Time.deltaTime;
                
            }
	    }
    }

}