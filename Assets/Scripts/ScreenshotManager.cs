using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenshotManager : MonoBehaviour
{
    public RawImage screenshotDisplay;
    private bool isScreenshotVisible = true;

    // Start is called before the first frame update
    void Start()
    {
        screenshotDisplay.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            TakeScreenshot();
            screenshotDisplay.gameObject.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            ToggleVisibility();
        }
    }

    void TakeScreenshot()
    {
        StartCoroutine(CaptureScreenshot());
    }

    System.Collections.IEnumerator CaptureScreenshot()
    {
        yield return new WaitForEndOfFrame();

        Texture2D screenshotTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

        screenshotTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenshotTexture.Apply();

        screenshotDisplay.texture = screenshotTexture;
        screenshotDisplay.gameObject.SetActive(true);
        isScreenshotVisible = true;
    }

    void ToggleVisibility()
    {
        isScreenshotVisible = !isScreenshotVisible;
        screenshotDisplay.gameObject.SetActive(isScreenshotVisible);
    }
}
