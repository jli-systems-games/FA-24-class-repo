using UnityEngine;

public class CameraAnimationController : MonoBehaviour
{
    public Animator animator;           
    public Transform cameraParent;   
    public string walkAnimation = "camera walk";   
    public string idleAnimation = "Camera idle";  
    public string cameraAnimation = "Camera";    
    public string camera2Animation = "Camera2";

    bool iswalking=false;
    bool idle = true;
    bool cameraDown = false;

    void Update()
    {
        
        float cameraY = cameraParent.localPosition.y;

        
        if (cameraY > -0.1f && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)
                               || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.RightArrow)))
        {
            Debug.Log("walk");
            if (cameraDown == false)
            {
                animator.Play(walkAnimation);
            }
            iswalking = true;
            idle = false;
            
        }

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D)
                               || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        //else
        {
            Debug.Log("Idle");
            if (cameraDown == false)
            {
                animator.Play(idleAnimation);
            }
            idle = true;
            iswalking = false;


        }

       
        if (cameraY > -0.1f && Input.GetKeyDown(KeyCode.N))//&& !iswalking && idle
        {

            cameraDown = false;
            animator.Play(cameraAnimation);
            Debug.Log("Y" + cameraParent.localPosition.y);
            Debug.Log("DOWN");

        }


       
        if (cameraY == -0.1f && Input.GetKeyDown(KeyCode.M) && !iswalking && idle)//&& !iswalking && !idle
        {
           cameraDown = true;
            animator.Play(camera2Animation);
            Debug.Log("UP");

        }
   
    }
}
