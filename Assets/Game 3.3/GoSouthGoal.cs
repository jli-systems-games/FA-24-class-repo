using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoSouthGoal : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Goal"))
        {
            GameObject.Find("GameManager").GetComponent<GoSouth>().Win();
        }
        else if (collision.gameObject.CompareTag("NotGoal"))
        {
            GameObject.Find("GameManager").GetComponent<GoSouth>().Lose();
        }
    }
}
