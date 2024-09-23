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
    [SerializeField] private Camera photoCam;
    [SerializeField] private Shader camShader;

    private bool viewingPhoto,cameraON;
    private Texture2D screenCapture;
    int clicked = 0;
    void Start()
    {
        
        //screenCapture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
    }

    
    void Update()
    {
        Debug.Log(photoCam.scaledPixelHeight);
        Debug.Log(Screen.width);
       
        if(Input.GetMouseButtonDown(0))
        {   clicked++;
            Debug.Log(clicked);
            //turn on camera;
            if (!cameraON && clicked == 1)
            {
                TurnOnCam();
                
            }else if(cameraON && clicked == 2) 
            {
                Debug.Log("took picture");
                StartCoroutine(CapturePhoto());
            }
            /*else if(clicked >= 3)
            {
                TurnOffCam();
            }*/
            
            
            
        }
    }

    IEnumerator CapturePhoto()
    {
       
        viewingPhoto = true;
        
        yield return new WaitForEndOfFrame();


        //Rect regiontoRead = new Rect(0,0, photoCam.scaledPixelWidth, photoCam.scaledPixelHeight);

        captureImage();
        TurnOffCam ();
        showPhotos();
        yield return new WaitForSeconds(3.0f);
        RemovePhoto();
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
        Photocamera.SetActive(true);
        if (camShader != null)
        {
            photoCam.SetReplacementShader(camShader, string.Empty);
        }
        cameraON = true;
       

    }
    void TurnOffCam()
    {
        cameraON = false;
        photoCam.ResetReplacementShader();
        Photocamera.SetActive(false);
        clicked = 0;
    }
    void captureImage()
    {
        RenderTexture rendertext = photoCam.targetTexture;
        screenCapture = new Texture2D(rendertext.width, rendertext.height, TextureFormat.RGB24, false);

        RenderTexture.active = rendertext;
        screenCapture.ReadPixels(new Rect(0, 0, rendertext.width, rendertext.height), 0, 0, false);
        screenCapture.Apply();
        RenderTexture.active = null;
    }
}
