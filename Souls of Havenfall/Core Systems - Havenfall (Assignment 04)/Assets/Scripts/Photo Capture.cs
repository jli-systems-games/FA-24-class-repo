using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    [Header("Controls Text")]
    [SerializeField] private GameObject controlsText; // Reference to the controls text object

    private Texture2D screenCapture;
    private bool viewingPhoto;
    private bool cameraUIToggled = false; //Track the state of the camera UI
    private bool allowUIToggle = true;     // Control when UI can be toggled

    /*
    [Header("Text Display")]
    [SerializeField] private GameObject textUI; // Reference to the text UI object
    [SerializeField] private TextMeshProUGUI textDisplay; // Reference to the TextMeshProUGUI component
    */

    private void Start()
    {
        screenCapture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

        // Ensure the UI is initially hidden
        cameraUI.SetActive(cameraUIToggled);
        /*
        textUI.SetActive(false); // Hide the text UI initially
        */

        controlsText.SetActive(true); // Show controls text initially
    }

private void Update()
    {
        // Handle camera UI toggle, but only if not viewing a photo
        if (Input.GetKeyDown(KeyCode.C) && allowUIToggle)
        {
            cameraUIToggled = !cameraUIToggled;
            cameraUI.SetActive(cameraUIToggled);
            UpdateControlsTextVisibility(); // Update controls text visibility
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

        // Hide controls text when showing the photo
        controlsText.SetActive(false);
    }

    /*
    void ShowText()
    {
        // Raycast to check if the player is looking at a gravestone
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
        {
            if (hit.collider.CompareTag("Gravestone")) // Check if the hit object is a gravestone
            {
                Gravestone gravestone = hit.collider.GetComponent<Gravestone>();
                if (gravestone != null)
                {
                    textDisplay.text = gravestone.noteText; // Set the text from the gravestone
                    Debug.Log($"Displaying text: {gravestone.noteText}"); // Log the text
                }
                else
                {
                    Debug.Log("Gravestone component not found.");
                }
            }
            else
            {
                Debug.Log("No gravestone hit.");
            }
        }
        else
        {
            Debug.Log("Raycast did not hit any object.");
        }

        textUI.SetActive(true); // Show the text UI
    }*/

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
        /*
        textUI.SetActive(false); // Hide the text UI when the photo is removed
        */
        allowUIToggle = true;  // Re-enable UI toggling after photo is removed
        cameraUI.SetActive(cameraUIToggled); // Show or hide UI based on the toggle state
        UpdateControlsTextVisibility(); // Update controls text visibility
    }

    private void UpdateControlsTextVisibility()
    {
        // Show controls text if neither the camera UI nor the photo is active
        controlsText.SetActive(!cameraUIToggled && !viewingPhoto);
    }

}

