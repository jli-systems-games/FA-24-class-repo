using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    public float dieTime;
    void Start()
    {
        Destroy(gameObject, dieTime);
    }

}
