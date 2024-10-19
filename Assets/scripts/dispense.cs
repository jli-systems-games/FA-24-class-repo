using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dispense : MonoBehaviour
{
    public GameObject ice;
    public GameObject salt;

    public GameObject icePrefab;
    public GameObject saltPrefab;


    public void dispenseIce()
    {
        Instantiate(icePrefab);
        ice = GameObject.FindWithTag("ice");
        StartCoroutine(delayIce());
    }

    private IEnumerator delayIce()
    {
        yield return new WaitForSeconds(4f);
        Destroy(ice);
        
    }

    public void dispenseSalt()
    {
        Instantiate(saltPrefab);
        salt = GameObject.FindWithTag("salt");
        StartCoroutine(delaySalt());
    }

    private IEnumerator delaySalt()
    {
        yield return new WaitForSeconds(4f);
        Destroy(salt);
    }

}
