using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snowballField : MonoBehaviour
{
    public bool PickUpArea;

    public Animator animator;
    public GameObject snowBall;
    public Rigidbody snowBallrb;
    public GameObject rightHand;

    public float throwForce = 500f;

    void Update()
    {
        if(Input.GetMouseButtonDown(1) && PickUpArea == true)
        {
            StartCoroutine(snowballTriggerDetection());
            animator.SetTrigger("throw");
        }
    }
    
    private IEnumerator snowballTriggerDetection()
    {
        GameObject varGameObject = GameObject.FindWithTag("Player");
		varGameObject.GetComponent<Player>().enabled = false;
        yield return new WaitForSeconds(1f);
        PickUpSnowball();
        yield return new WaitForSeconds(1.5f);
        ThrowSnowball();
        yield return new WaitForSeconds(2.5f);
        varGameObject.GetComponent<Player>().enabled = true;
    }

    public void PickUpSnowball()
    {
        snowBall.transform.SetParent(rightHand.transform);
        snowBall.transform.localPosition = new Vector3(0f, 0f, 0f);
        snowBall.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    void ThrowSnowball()
    {
        snowBall.layer = 0;
        snowBallrb.isKinematic = false;
        snowBall.transform.parent = null;
        snowBallrb.AddForce(transform.forward * throwForce);
        snowBall = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("snowball"))
        {
            PickUpArea = true;
            snowBall = other.gameObject;
            snowBallrb = snowBall.GetComponent<Rigidbody>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PickUpArea = false;
    }
}
