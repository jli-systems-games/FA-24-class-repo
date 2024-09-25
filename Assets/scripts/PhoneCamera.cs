using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneCamera : MonoBehaviour
{
    public Camera phoneCamera;
    public GameObject flashImage;
    public GameObject cameraUI;
    public AudioSource cameraSound;
    private bool isCameraMode = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleCameraMode();
        }

        if (isCameraMode && Input.GetKeyDown(KeyCode.F))
        {
            TakePicture();
        }
    }

    public void ToggleCameraMode()
    {
        isCameraMode = !isCameraMode;

        if (isCameraMode)
        {
            phoneCamera.enabled = true;
            phoneCamera.gameObject.SetActive(true);
            cameraUI.SetActive(true);
        }
        else
        {
            phoneCamera.enabled = false;
            phoneCamera.gameObject.SetActive(false);
            cameraUI.SetActive(false);
        }
    }

    public void TakePicture()
    {
        StartCoroutine(FlashEffect());
        if (cameraSound != null)
        {
            cameraSound.Play();
        }
    }

    IEnumerator FlashEffect()
    {
        flashImage.SetActive(true);
        yield return new WaitForSeconds(0.08f);
        flashImage.SetActive(false);
    }
}
