using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoWestGoal : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Goal"))
        {
            GameObject.Find("GameManager").GetComponent<GoWest>().Win();
        }
        else if (collision.gameObject.CompareTag("NotGoal"))
        {
            GameObject.Find("GameManager").GetComponent<GoWest>().Lose();
        }
    }
}
