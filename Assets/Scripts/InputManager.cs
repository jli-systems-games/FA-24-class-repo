using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public GameObject[] testHands;
    public Transform testHandPos;

    public GameObject leftHandUI;
    public GameObject rightHandUI;

    private Animator leftHandAnimator;
    private Animator rightHandAnimator;

    private bool canUseLeftHand = true;
    private bool canUseRightHand = true;

    public GameObject hand1;
    public GameObject hand2;
    public GameObject hand3;
    public GameObject hand4;

    public GameObject hand7;
    public GameObject hand8;
    public GameObject hand9;
    public GameObject hand0;

    public Transform anchor1;
    public Transform anchor2;
    public Transform anchor3;
    public Transform anchor4;

    public Transform anchor7;
    public Transform anchor8;
    public Transform anchor9;
    public Transform anchor0;
    void Start()
    {
        leftHandAnimator = leftHandUI.GetComponent<Animator>();
        rightHandAnimator = rightHandUI.GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            int randomIndex = Random.Range(0, testHands.Length);
            Instantiate(testHands[randomIndex], testHandPos.position, Quaternion.identity);
        }



        if (canUseLeftHand && Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            leftHandAnimator.SetTrigger("Trigger");
            Instantiate(hand1, anchor1.position, Quaternion.identity);
        }
        if (canUseLeftHand && Input.GetKeyDown(KeyCode.Alpha2))
        {
            leftHandAnimator.SetTrigger("Trigger");
            Instantiate(hand2, anchor2.position, Quaternion.identity);

        }
        if (canUseLeftHand && Input.GetKeyDown(KeyCode.Alpha3))
        {
            leftHandAnimator.SetTrigger("Trigger");
            Instantiate(hand3, anchor3.position, Quaternion.identity);
        }
        if (canUseLeftHand && Input.GetKeyDown(KeyCode.Alpha4))
        {
            leftHandAnimator.SetTrigger("Trigger");
            Instantiate(hand4, anchor4.position, Quaternion.identity);
        }

        if (canUseRightHand && Input.GetKeyDown(KeyCode.Alpha7))
        {
            rightHandAnimator.SetTrigger("Trigger");
            Instantiate(hand7, anchor7.position, Quaternion.identity);
        }
        if (canUseRightHand && Input.GetKeyDown(KeyCode.Alpha8))
        {
            rightHandAnimator.SetTrigger("Trigger");
            Instantiate(hand8, anchor8.position, Quaternion.identity);
        }
        if (canUseRightHand && Input.GetKeyDown(KeyCode.Alpha9))            
        {
            rightHandAnimator.SetTrigger("Trigger");
            Instantiate(hand9, anchor9.position, Quaternion.identity);            
        }
        if (canUseRightHand && Input.GetKeyDown(KeyCode.Alpha0))
        {
            rightHandAnimator.SetTrigger("Trigger");
            Instantiate(hand0, anchor0.position, Quaternion.identity);
        }
    }
}
