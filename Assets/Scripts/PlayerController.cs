using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Basic settings")]
    [SerializeField] float speed;
    [SerializeField] float strafeSpeed; //speed when to move left and right
    [SerializeField] float jumpForce;
    [SerializeField] float rotationThreshold; 

    [Header("Others")]
    [SerializeField] Rigidbody hip;
    [SerializeField] Transform torso, COM;
    [SerializeField] Animator _animate;
    [SerializeField] Customization _custom;
    public bool isGrounded, leftArmHeld, rigthArmHeld;
    public float upwardForce, backForce;
    
    [SerializeField] TMP_Text _debug, _deBugBOOL;
    Rigidbody torRb;

    bool isClimbing;
    float ogSpeed,currVel,maxVelocity;
    void Start()
    {
        torRb = torso.GetComponent<Rigidbody>();
        EventManager.Climb += startClimbing;
        EventManager.stopClimb += checkClimb;
        EventManager.Reset += Reset;

        ogSpeed = speed;
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Reset();
        }
       
        //for Debug;

        float vel = torRb.velocity.y;
       /* _debug.text = jumpForce.ToString();
        _deBugBOOL.text = isGrounded.ToString();*/

        Mathf.Clamp(vel, -0.5f, 5f);

       
              float rotationDiff = Quaternion.Angle(hip.transform.rotation, torso.transform.rotation);
              torRb.AddForce(new Vector3(0, upwardForce,0));

              if (Input.GetKey(KeyCode.W))
              {
                  //Debug.Log("it is rotating " + rotationDiff + "degree");
                 _animate.SetBool("walking", true);
                 if (Input.GetKey(KeyCode.LeftShift))
                  {
                    hip.AddForce(hip.transform.forward * speed * 1.5f);
                    torRb.AddForce(-hip.transform.forward * 100f);
                    _animate.SetFloat("RunSpeed", 1.5f);

                  }
                else
                  {
                     hip.AddForce(hip.transform.forward * speed);
                    _animate.SetFloat("RunSpeed", 1f);

                     torRb.AddForce(-hip.transform.forward * backForce);
                   }

              }
             else
              {
                  _animate.SetBool("walking", false);
              }
        

                    if (Input.GetKey(KeyCode.S))
                    {
                        hip.AddForce(-hip.transform.forward * speed);
                        _animate.SetBool("backing", true);
                    }
                    else
                    {
                        // _animate.SetBool("walking", false);
                        _animate.SetBool("backing", false);
                    }

                    if (Input.GetKey(KeyCode.D))
                    {
                        hip.AddForce(hip.transform.right * strafeSpeed);
                        _animate.SetBool("RightSteping", true);

                    }
                    else
                    {
                        _animate.SetBool("RightSteping", false);
                    }

                    if (Input.GetKey(KeyCode.A))
                    {
                        hip.AddForce(-hip.transform.right * strafeSpeed);
                        _animate.SetBool("LeftSteping", true);
                    }
                    else
                    {
                       _animate.SetBool("LeftSteping", false);

                    }

                    if (Input.GetAxis("Jump") >0)
                    {
                        if (isGrounded)
                        {   
                            hip.AddForce(new Vector3(0, jumpForce, transform.forward.z), ForceMode.Impulse);
                           
                            if(!isClimbing)
                            {   
                                //Debug.Log("Climbing");
                                
                                isGrounded = false;

                            }
                  
                             
                             
                        }
           

                    } 
            if (isClimbing)
            {
                //recalculate upwardForce and direction
                        
               jumpForce = Mathf.Clamp(jumpForce, 50f, 150f);
               if(!leftArmHeld && !rigthArmHeld)
                {
                    endClimbing();
                    isClimbing = false;
                }
         
           }
        

      

    } 
    void startClimbing(int m)
    {   
        isClimbing = true;
        jumpForce = jumpForce * 0.1f;
        speed = speed * 0.75f;

        if(m == 0)
        {
            leftArmHeld = true;
        }else if (m == 1)
        {
            rigthArmHeld = true;
        }
    }
    void checkClimb(int m)
    {
        if (m == 0)
        {
            leftArmHeld = false;
        }
        else if (m == 1)
        {
            rigthArmHeld = false;
        }
    }
    void endClimbing()
    {
        
        jumpForce = 760f;
        speed = ogSpeed;
    }
    private void Reset()
     {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
}
