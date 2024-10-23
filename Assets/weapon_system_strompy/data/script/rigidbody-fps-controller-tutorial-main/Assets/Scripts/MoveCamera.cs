using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] Transform cameraPosition = null;

    void Update()
    {
        transform.position = cameraPosition.position;
    }

    private void Start()
    {
        weaponCam.enabled = false;
        weaponCam.enabled = true;
    }

    public Camera weaponCam;
}
