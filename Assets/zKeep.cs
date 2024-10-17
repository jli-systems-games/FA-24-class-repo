using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zKeep : MonoBehaviour
{
    public GameObject item;

    void Start()
    {
        DontDestroyOnLoad(item);
    }

}
