using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoCapture : MonoBehaviour
{
    [Header("Photo Taker")]
    [SerializeField] private Image photoDisplayArea;
    [SerializeField] private GameObject photoFrame;
    [SerializeField] private GameObject cameraUI;

    [Header("Flash Effect")]
    [SerializeField] private GameObject cameraFlash;
    [SerializeField] private float flashTime;

    [Header("Photo Fader Effect")]
    [SerializeField] private Animator fadingAnimation;

    [Header("Camera Shutter")]
    [SerializeField] private AudioSource cameraAudio;

    private Texture2D screenCapture;
    private bool viewingPhoto;
    private bool cameraUIToggled = false; //Track the state of the camera UI
    private bool allowUIToggle = true;     // Control when UI can be toggled

    private void Start()
    {
        screenCapture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

        // Ensure the UI is initially hidden
        cameraUI.SetActive(cameraUIToggled);
    }

    private void Update()
    {
        // Handle camera UI toggle, but only if not viewing a photo
        if (Input.GetKeyDown(KeyCode.C) && allowUIToggle)
        {
            cameraUIToggled = !cameraUIToggled;
            cameraUI.SetActive(cameraUIToggled);
        }

        // Handle photo capture with right mouse button, but only if the camera UI is active
        if (Input.GetMouseButtonDown(1))
        {
            if (cameraUI.activeSelf && !viewingPhoto)
            {
                StartCoroutine(CapturePhoto());
            }
            else if (viewingPhoto)
            {
                RemovePhoto(); // Allow photo to be removed regardless of camera UI state
            }
        }
    }

    IEnumerator CapturePhoto()
    {
        cameraUI.SetActive(false); // Hide UI during photo capture
        allowUIToggle = false;     // Disable UI toggling while capturing/viewing photo
        viewingPhoto = true;

        yield return new WaitForEndOfFrame();

        Rect regionToRead = new Rect(0, 0, Screen.width, Screen.height);

        screenCapture.ReadPixels(regionToRead, 0, 0, false);
        screenCapture.Apply();
        ShowPhoto();
    }

    void ShowPhoto()
    {
        Sprite photoSprite = Sprite.Create(screenCapture, new Rect(0.0f, 0.0f, screenCapture.width, screenCapture.height), new Vector2(0.5f, 0.5f), 100.0f);
        photoDisplayArea.sprite = photoSprite;

        photoFrame.SetActive(true);

        StartCoroutine(CameraFlashEffect());
        fadingAnimation.Play("PhotoFade");
    }

    IEnumerator CameraFlashEffect()
    {
        cameraAudio.Play();
        cameraFlash.SetActive(true);
        yield return new WaitForSeconds(flashTime);
        cameraFlash.SetActive(false);
    }

    void RemovePhoto()
    {
        viewingPhoto = false;
        photoFrame.SetActive(false);
        cameraUI.SetActive(true);
        allowUIToggle = true;  // Re-enable UI toggling after photo is removed
        cameraUI.SetActive(cameraUIToggled); // Show or hide UI based on the toggle state
    }
}
