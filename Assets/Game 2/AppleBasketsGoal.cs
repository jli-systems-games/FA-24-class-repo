using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleBasketsGoal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Goal"))
        {
            GameObject.Find("GameManager").GetComponent<AppleBaskets>().Win();
        }
        else if (collision.gameObject.CompareTag("NotGoal"))
        {
            GameObject.Find("GameManager").GetComponent<AppleBaskets>().Lose();
        }
    }
}
