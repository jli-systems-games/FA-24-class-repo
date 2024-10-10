using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectThrown : MonoBehaviour
{
    public Animator alice;

    public GameObject snowball;
    public GameObject particleBurst;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("alice"))
        {
            Debug.Log("hit alice");
            snowball.gameObject.SetActive(false);
            particleBurst.gameObject.SetActive(true);
            alice.SetTrigger("gotHit");
        }
    }
}
