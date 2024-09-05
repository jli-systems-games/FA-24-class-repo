using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dontDestroy : MonoBehaviour
{

    void Start()
    {
        for (int i = 0; i < Object.FindObjectsOfType<dontDestroy>().Length; i++)
        {
            if(Object.FindObjectsOfType<dontDestroy>()[i] != this)
            {
                if(Object.FindObjectsOfType<dontDestroy>()[i].name == gameObject.name)
                {
                Destroy(gameObject);
                }
            }
        }

        
        DontDestroyOnLoad(this.gameObject);
    }
}
