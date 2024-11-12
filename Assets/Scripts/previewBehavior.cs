using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class previewBehavior : MonoBehaviour
{
    Vector3 offset;
    float x;
    Spawning _spawn;

    void Start()
    {
        _spawn = GetComponent<Spawning>();
    }
   
    public void RotateInput(InputAction.CallbackContext context)
    {
        if (_spawn.rotatible)
        {
            if (context.started)
           {   
               x = context.ReadValue<float>();
               transform.Rotate(Vector3.forward, 30f * x);
           }
        }
        
       
    }
    public void followMouse(Vector3 _offset)
    {   
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = pos + _offset;

    }
}
