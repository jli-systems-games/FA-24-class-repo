using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowScript : MonoBehaviour
{


    
    [SerializeField] Transform holdingPoint, plyCam;
    [SerializeField] GameObject ball;
    public float throForce = 5, upwardForce = 5;
  
    public float MaxBallSpeed = 350;
    private Vector3 angle;

    private bool thrown, holding;
    private Vector3 newPosition, resetPos;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        resetPos = transform.position;
        eventManager.switchItems += Throw;
        //ResetBall();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(thrown);
       /* if(Input.GetMouseButtonDown(0) && !thrown)
        {
                 
            Throw();
            
        }*/
    }
    private void OnMouseDown()
    {  
        if (!thrown)
        {
            
             
        }
       
        
    }
   
    private void OnMouseUp()
    {
        
    }
    void ResetBall()
    {
        /*angle = Vector3.zero;
       
        BallSpeed = 0;
      
        thrown = holding = false;
        rb.velocity = Vector3.zero;
        rb.useGravity = false;
        transform.position = resetPos;*/

        thrown = false;
    }
    void Throw(GameObject obj)
    {
        thrown = true;

        GameObject projectile = Instantiate(obj, holdingPoint.position, plyCam.rotation);

        Vector3 addedForce = plyCam.transform.forward * throForce + transform.up * upwardForce;
        //Debug.Log(addedForce);
        Rigidbody _rb = projectile.GetComponent<Rigidbody>();
        _rb.isKinematic = false;
        _rb.AddForce(addedForce, ForceMode.Impulse);


        Invoke(nameof(ResetBall), 2f);
    }
   
   
}
