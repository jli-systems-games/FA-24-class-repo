using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] Transform torso;
    [SerializeField] Animator _animate;
    [SerializeField] Customization _custom;
    public bool isGrounded;
    public float upwardForce;
    Rigidbody torRb;
    float turnAng;
    void Start()
    {
        torRb = torso.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Reset();
        }
        if (_custom.done)
        {
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
                    }
                        else
                        {
                            hip.AddForce(hip.transform.forward * speed);

                            torRb.AddForce(-hip.transform.forward * 450f);
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
                    }

                    if (Input.GetKey(KeyCode.A))
                    {
                        hip.AddForce(-hip.transform.right * strafeSpeed);
                    }

                    if (Input.GetAxis("Jump") >0)
                    {
                        if (isGrounded)
                        {
                             hip.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
                             isGrounded = false;
                        }
           

                    }
        }

      

    } 
    private void Reset()
     {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
}
