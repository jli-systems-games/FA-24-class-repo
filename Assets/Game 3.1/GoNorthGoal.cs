using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoNorthGoal : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Goal"))
        {
            GameObject.Find("GameManager").GetComponent<GoNorth>().Win();
        }
        else if (collision.gameObject.CompareTag("NotGoal"))
        {
            GameObject.Find("GameManager").GetComponent<GoNorth>().Lose();
        }
    }
}
