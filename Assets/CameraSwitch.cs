using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public GameObject totalCam;
    public GameObject headCam;
    public GameObject bodyCam;

    public GameObject faceObjects;
    public GameObject clothesObjects;

    public GameObject finalText;
    public List<GameObject> buttons = new List<GameObject>();

    public GameObject restartButton;

    // Start is called before the first frame update
    void Start()
    {
        faceObjects.SetActive(true);
        headCam.SetActive(true);
        bodyCam.SetActive(false);

        clothesObjects.SetActive(false);
        totalCam.SetActive(false);

        finalText.SetActive(false);
        restartButton.SetActive(false);
    }

    public void ZFaceDeco()
    {
        faceObjects.SetActive(true);
        headCam.SetActive(true);
        bodyCam.SetActive(false);

        clothesObjects.SetActive(false);
        totalCam.SetActive(false);

        finalText.SetActive(false);

        Debug.Log("click");
    }

    public void ZBodyDeco()
    {
        faceObjects.SetActive(true);
        headCam.SetActive(false);
        bodyCam.SetActive(true);

        clothesObjects.SetActive(false);
        totalCam.SetActive(false);

        finalText.SetActive(false);

        Debug.Log("click");
    }

    public void ZClothesDeco()
    {
        faceObjects.SetActive(false);
        headCam.SetActive(false);
        bodyCam.SetActive(false);

        clothesObjects.SetActive(true);
        totalCam.SetActive(true);

        finalText.SetActive(false);

        Debug.Log("click");
    }

    public void FinalLook()
    {
        faceObjects.SetActive(false);
        headCam.SetActive(false);
        bodyCam.SetActive(false);

        clothesObjects.SetActive(true);
        totalCam.SetActive(true);

        finalText.SetActive(true);
        restartButton.SetActive(true);


        foreach (GameObject button in buttons)
        {
            button.SetActive(false);
        }

        Debug.Log("click");
    }
}
