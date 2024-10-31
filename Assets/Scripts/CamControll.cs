using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CamControll : MonoBehaviour
{
    public float rotationSpeed = 1f;
    public Transform target;
    [SerializeField] Customization custom;
    
    float mouseX, mouseY, mouseSenstivity;

    public ConfigurableJoint hipJoint;
    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        if(scene.name == "Climb")
        {
            //Debug.Log("locking"); 
            Cursor.lockState = CursorLockMode.Locked;
        }
       
        mouseSenstivity = 1.5f;
        EventManager.Climb += decreaseSensitivity;
        EventManager.stopClimb += restoreSensitivity;
    }

    // Update is called once per frame
    void FixedUpdate()
    {  
        CamBehavior();
  
    }

    void CamBehavior()
    {
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        mouseY += Input.GetAxis("Mouse Y") * rotationSpeed;
        mouseY = Mathf.Clamp(mouseY, -35, 60);
        //mouseX = Mathf.Clamp(mouseX, -35, 60);

        Quaternion rootRotation = Quaternion.Euler(mouseY * -1f, mouseX , 0);
        target.rotation = rootRotation;

        hipJoint.targetRotation = Quaternion.Euler(0, -mouseX * mouseSenstivity, 0);
    }
    void decreaseSensitivity(int m)
    {
        mouseSenstivity = 1f;
    }
    void restoreSensitivity(int m)
    {
        mouseSenstivity = 1.5f;
    }
}
