using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera[] cameras; 
    private int currentCameraIndex = 0; 

    void Start()
    {
        
        for (int i = 0; i < cameras.Length; i++)
        {
            bool isCurrent = (i == currentCameraIndex);
            cameras[i].enabled = isCurrent;
            cameras[i].GetComponent<AudioListener>().enabled = isCurrent;
        }
    }

    public void SwitchCamera()
    {
      
        cameras[currentCameraIndex].enabled = false;
        cameras[currentCameraIndex].GetComponent<AudioListener>().enabled = false;

      
        currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;

       
        cameras[currentCameraIndex].enabled = true;
        cameras[currentCameraIndex].GetComponent<AudioListener>().enabled = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            SwitchCamera();
        }
    }
}
