using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class TakePicture : MonoBehaviour
{
    public Image spaceIndic;
    public Canvas canvas;

    public GameObject cameraObj;

    private Vector3 _position;
    // Start is called before the first frame update
    void Start()
    {
        spaceIndic.GetComponent<Image>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        spaceIndic.GetComponent<Image>().enabled = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            _position = transform.position;
            StartCoroutine(takePic());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        spaceIndic.GetComponent<Image>().enabled = false;
    }

    public IEnumerator takePic()
    {
        canvas.enabled = false;
        string folderPath = "Assets/Images/";
        if (!System.IO.Directory.Exists(folderPath))
        {
            System.IO.Directory.CreateDirectory(folderPath);
        }
        cameraObj.GetComponent<Animator>().Play("take-picture");
        yield return new WaitForSeconds(.4f);

        cameraObj.SetActive(false);
        transform.position = _position;

        yield return new WaitForSeconds(.1f);
        var imageName = "Pigeon_" + System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss") + ".png";
        ScreenCapture.CaptureScreenshot(System.IO.Path.Combine(folderPath, imageName), 2);

        Debug.Log(folderPath + imageName);

        yield return new WaitForSeconds(.5f);

        cameraObj.SetActive(true);

        canvas.enabled = true;
    }
}
