using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class previewBehavior : MonoBehaviour
{
    Vector3 offset;
    Vector2 InputVect;

    void Start()
    {
        offset = new Vector3(0, 0, 10);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = pos + offset;
    }
    void RotateInput(InputAction.CallbackContext context)
    {
        InputVect = context.ReadValue<Vector2>();
        Debug.Log("x" + InputVect.x + "y" + InputVect.y);
    }
}
