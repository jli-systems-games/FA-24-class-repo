using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
            StartCoroutine(stopMovement());
        }
    }

    private IEnumerator stopMovement()
    {
        GameObject varGameObject = GameObject.FindWithTag("alice");
		varGameObject.GetComponent<aiPatrol>().enabled = false;
        varGameObject.GetComponent<NavMeshAgent>().enabled = false;
        yield return new WaitForSeconds(4.6f);
        varGameObject.GetComponent<NavMeshAgent>().enabled = true;
        varGameObject.GetComponent<aiPatrol>().enabled = true;
    }
}
