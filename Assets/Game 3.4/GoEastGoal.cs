using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoEastGoal : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Goal"))
        {
            GameObject.Find("GameManager").GetComponent<GoEast>().Win();
        }
        else if (collision.gameObject.CompareTag("NotGoal"))
        {
            GameObject.Find("GameManager").GetComponent<GoEast>().Lose();
        }
    }
}
