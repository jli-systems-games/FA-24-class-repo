using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleBasketsGoal : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Goal"))
        {
            GameObject.Find("AppleBaskets").GetComponent<AppleBaskets>().Win();
        }
        else if (collision.gameObject.CompareTag("NotGoal"))
        {
            GameObject.Find("AppleBaskets").GetComponent<AppleBaskets>().Lose();
        }
    }
}
