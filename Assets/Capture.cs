using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Capture : MonoBehaviour
{
    [Header("Photo Taker")]
    [SerializeField] private Image photoDisplayArea;
    [SerializeField] private GameObject photoFrame;
    [SerializeField] private GameObject Photocamera;

    private bool viewingPhoto,cameraON;
    private Texture2D screenCapture;
    
    void Start()
    {
       screenCapture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24,false); 
    }

    
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //turn on camera;
            if (!cameraON)
            {
                TurnOnCam();
                //need to actually detect the double clicks;
                if(!viewingPhoto && cameraON)
                {
                    StartCoroutine(CapturePhoto());
                }
                else if(viewingPhoto)
                {
                    RemovePhoto();

                }
            }
            else
            {

            }
            
            
            
        }
    }

    IEnumerator CapturePhoto()
    {
        Photocamera.SetActive(false);
        viewingPhoto = true;
        cameraON = false;
        yield return new WaitForEndOfFrame();

        Rect regiontoRead = new Rect(0,0, Screen.width, Screen.height);
        screenCapture.ReadPixels(regiontoRead, 0, 0, false);
        screenCapture.Apply();

        showPhotos();
    }

    void showPhotos()
    {
        Sprite photoSprite = Sprite.Create(screenCapture, new Rect(0f, 0f, screenCapture.width, screenCapture.height), new Vector2(0.5f, 0.5f), 100.0f);
        photoDisplayArea.sprite = photoSprite;

        photoFrame.SetActive(true);
    }

    void RemovePhoto()
    {
        viewingPhoto = false;
        photoFrame.SetActive(false);    
        //Camera UI

    }

    void TurnOnCam()
    {
        cameraON = true;
        Photocamera.SetActive(true);

    }
    void TurnOffCam()
    {
        cameraON = false;
        Photocamera.SetActive(false);
    }
}
