using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectThrown : MonoBehaviour
{
    public snowballField throwBool;
    public GameObject snowball;
    public GameObject particleBurst;

    void OnCollisionEnter(Collision other)
    {
        if (throwBool.thrown == true)
        {
            snowball.gameObject.SetActive(false);
            particleBurst.gameObject.SetActive(true);
        }
    }
}
