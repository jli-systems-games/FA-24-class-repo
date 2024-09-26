using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zoomAnim : MonoBehaviour
{

    public FirstPersonController zoomed;
    Animator animator;

    public GameObject fpc;
    public GameObject playerCamera;

    public GameObject armsAndCam;


    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (zoomed.isZoomed == true)
        {
            animator.SetBool("zoomIn", true);
            //armsAndCam.transform.parent = playerCamera.transform;

        }
        if (zoomed.isZoomed == false)
        {
            animator.SetBool("zoomIn", false);
            //armsAndCam.transform.parent = fpc.transform;
        }
        
    }
}
