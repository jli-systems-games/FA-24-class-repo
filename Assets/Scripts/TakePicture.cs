using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class TakePicture : MonoBehaviour
{
    public Image spaceIndic;
    public Button questionButton;
    public Canvas canvas;

    public GameObject cameraObj;
    public GameObject cameraMesh;
    public GameObject prompt;

    private Vector3 cameraPos;

    // Start is called before the first frame update
    void Start()
    {
        spaceIndic.gameObject.SetActive(false);
        prompt.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        spaceIndic.gameObject.SetActive(true);
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.Q)) 
        {
            //StartCoroutine(takePic());
            StartCoroutine(promptScreenshot());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        spaceIndic.gameObject.SetActive(false);
    }

    private IEnumerator promptScreenshot()
    {
        spaceIndic.gameObject.SetActive(false);
        questionButton.gameObject.SetActive(false);
        cameraPos = cameraObj.transform.position;
        cameraObj.GetComponent<Animator>().Play("take-picture");

        yield return new WaitForSeconds(.67f);

        cameraMesh.GetComponent<MeshRenderer>().enabled = false;
        prompt.SetActive(true);

        Time.timeScale = 0;

        yield return new WaitForSecondsRealtime(1);

        prompt.SetActive(false); 
        
        yield return new WaitForSecondsRealtime(1);

        Time.timeScale = 1;
        questionButton.gameObject.SetActive(true);
        cameraMesh.GetComponent<MeshRenderer>().enabled = true;
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
        yield return new WaitForSeconds(.67f);

        cameraMesh.GetComponent<MeshRenderer>().enabled = false;

        yield return new WaitForSeconds(.1f);
        var imageName = "Pigeon_" + System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss") + ".png";
        ScreenCapture.CaptureScreenshot(System.IO.Path.Combine(folderPath, imageName), 2);

        Debug.Log(folderPath + imageName);

        yield return new WaitForSeconds(.5f);

        cameraMesh.GetComponent<MeshRenderer>().enabled = true;

        canvas.enabled = true;
    }
}
