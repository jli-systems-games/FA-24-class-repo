using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zoomAnim : MonoBehaviour
{

    public FirstPersonController zoomed;
    public FirstPersonController walking;
    Animator animator;

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

        }
        if (zoomed.isZoomed == false)
        {
            animator.SetBool("zoomIn", false);
        }
        if (walking.isWalking == true)
        {
            animator.SetBool("isWalkingg", true);

        }
        if (walking.isWalking == false)
        {
            animator.SetBool("isWalkingg", false);
        }
        
    }
}
