using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hide : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(hideObject());
    }

    private IEnumerator hideObject()
    {
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
    }
}
