using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoCapture : MonoBehaviour
{
    [Header("Photo Taker")]
    [SerializeField] private Image photoDisplayArea;
    [SerializeField] private Camera photoCamera;
    [SerializeField] private RenderTexture cameraRenderTexture;
    [SerializeField] private GameObject cameraMenu;
    [SerializeField] private AudioSource captureSound;
    [SerializeField] private GameObject photoFrame;

    [Header("Flash Effect")]
    [SerializeField] private GameObject cameraFlash;
    [SerializeField] private float flashtime;

    [Header("Fading")]
    [SerializeField] private Animator fadingAnimation;

    [Header("Elements to Capture")]
    [SerializeField] private GameObject newSprite;
    [SerializeField] private GameObject face;

    private Texture2D screenCapture;
    private bool viewingPhoto;
    private bool canCaptureFace; 

    private void Start()
    {
        screenCapture = new Texture2D(cameraRenderTexture.width, cameraRenderTexture.height, TextureFormat.RGB24, false);
        newSprite.SetActive(false); 
        face.SetActive(false); 
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (!viewingPhoto)
            {
                StartCoroutine(CapturePhoto());
            }
            else
            {
                RemovePhoto();
            }
        }
    }

    IEnumerator CapturePhoto()
    {
        captureSound.Play();

        viewingPhoto = true;

        cameraMenu.SetActive(false);

        
        newSprite.SetActive(true);
        if (canCaptureFace) 
        {
            face.SetActive(true);
        }

        yield return null; 

        RenderTexture.active = cameraRenderTexture;
        Rect regionToRead = new Rect(0, 0, cameraRenderTexture.width, cameraRenderTexture.height);
        screenCapture.ReadPixels(regionToRead, 0, 0, false);
        screenCapture.Apply();
        RenderTexture.active = null;

        ShowPhoto();

        newSprite.SetActive(false); 
        face.SetActive(false);     

        cameraMenu.SetActive(true);
    }

    IEnumerator CameraFlashEffect()
    {
        cameraFlash.SetActive(true);
        yield return new WaitForSeconds(flashtime);
        cameraFlash.SetActive(false);
    }

    void ShowPhoto()
    {
        Sprite photoSprite = Sprite.Create(screenCapture, new Rect(0.0f, 0.0f, screenCapture.width, screenCapture.height), new Vector2(0.5f, 0.5f), 100.0f);
        photoDisplayArea.sprite = photoSprite;

        photoFrame.SetActive(true);
        StartCoroutine(CameraFlashEffect());

        fadingAnimation.Play("camerafade");
    }

    void RemovePhoto()
    {
        viewingPhoto = false;
        photoFrame.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canCaptureFace = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canCaptureFace = false;
        }
    }
}
