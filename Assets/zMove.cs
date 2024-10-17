using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zMove : MonoBehaviour
{

    public GameObject item;


    // Start is called before the first frame update
    void Start()
    {
        item.transform.position = new Vector3(-642, -41, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
