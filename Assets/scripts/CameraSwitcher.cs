using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera[] cameras; // ���������
    private int currentCameraIndex = 0; // ��ǰ���������

    void Start()
    {
        // ȷ��ֻ�е�ǰ��������ã��������������
        for (int i = 0; i < cameras.Length; i++)
        {
            bool isCurrent = (i == currentCameraIndex);
            cameras[i].enabled = isCurrent;
            cameras[i].GetComponent<AudioListener>().enabled = isCurrent;
        }
    }

    public void SwitchCamera()
    {
        // ���õ�ǰ�����
        cameras[currentCameraIndex].enabled = false;
        cameras[currentCameraIndex].GetComponent<AudioListener>().enabled = false;

        // ������һ�������������
        currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;

        // ������һ�������
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
