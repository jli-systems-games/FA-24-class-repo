using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSelfDestroy : MonoBehaviour
{
    public float destroyTime = 5;
    void Start()
    {
        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    { 
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }
}
