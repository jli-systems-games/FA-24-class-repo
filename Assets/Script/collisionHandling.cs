using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class collisionHandling : MonoBehaviour
{
    NavigationManager nav;

    void Start()
    {
        nav = GetComponentInParent<NavigationManager>();
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        nav.Direction();
    }
}
