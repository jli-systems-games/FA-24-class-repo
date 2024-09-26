using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraManager : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            string timeStamp = DateTime.Now.ToString("HH_mm_ss");
            ScreenCapture.CaptureScreenshot("snapshot_" + timeStamp + ".png");
            Debug.Log("picture taken");
        }
    }
}
