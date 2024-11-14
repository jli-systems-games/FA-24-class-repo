using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

        transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z + .3f);

    }

}
