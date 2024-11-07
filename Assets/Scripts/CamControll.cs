using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CamControll : MonoBehaviour
{
    public float rotationSpeed = 1f;
    public Transform target;
    [SerializeField] Customization custom;

    float mouseX, mouseY;
    public float mouseSenstivity;
    Scene scene;
    public ConfigurableJoint hipJoint;
    void Start()
    {
        scene = SceneManager.GetActiveScene();
      
       
        mouseSenstivity = 0.75f;
        EventManager.Climb += decreaseSensitivity;
        EventManager.stopClimb += restoreSensitivity;
    }

    // Update is called once per frame
    void FixedUpdate()
    {  
        CamBehavior();
        //Debug.Log(mouseSenstivity);
    }

    void CamBehavior()
    {  
        if(scene.name == "Climb")
        {
            //Debug.Log("locking"); 
            Cursor.lockState = CursorLockMode.Locked;
        }
        //Cursor.lockState = CursorLockMode.Locked;
        mouseX += Input.GetAxis("Mouse X") * mouseSenstivity;
        mouseY += Input.GetAxis("Mouse Y") * (rotationSpeed / 2);
        mouseY = Mathf.Clamp(mouseY, -35, 60);
        //mouseX = Mathf.Clamp(mouseX, -35, 60);

        Quaternion rootRotation = Quaternion.Euler(mouseY * -1f, mouseX , 0);
        target.rotation = rootRotation;

        hipJoint.targetRotation = Quaternion.Euler(0, -mouseX, 0);
    }
    void decreaseSensitivity(int m)
    {
        mouseSenstivity = 0.7f;
    }
    void restoreSensitivity(int m)
    {
        mouseSenstivity = 1.5f;
    }
}
