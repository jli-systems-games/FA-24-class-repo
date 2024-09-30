using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCamera : MonoBehaviour
{
    public GameObject MessagesBackground;
    public GameObject MessagesOverlay;

    public GameObject CameraStuff;

    public GameObject BlackBox;
    public GameObject WhiteBox;

    private AudioSource audioSource;
    public AudioClip Snap;

    public bool Taken = true;

    public Texting Text;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ZSwitchToMessages()
    {
        MessagesBackground.SetActive(false);
        MessagesOverlay.SetActive(true);

        CameraStuff.SetActive(false);
    }

    public void ZSwitchToCamera()
    {
        MessagesBackground.SetActive(false);
        MessagesOverlay.SetActive(false);

        CameraStuff.SetActive(true);
    }

    public void ZTakePicture()
    {
        if (Taken == true)
        {
            BlackBox.SetActive(false);

            StartCoroutine(ZFlashOffOne());
            audioSource.PlayOneShot(Snap);
        }
        else if (Taken == false)
        {
            WhiteBox.SetActive(true);
            StartCoroutine(ZFlashOff());
            audioSource.PlayOneShot(Snap);
        }
    }

    private IEnumerator ZFlashOffOne()
    {
        yield return new WaitForSeconds(0.2f);
        Taken = false;
        WhiteBox.SetActive(false);
        ZSwitchToMessages();
        Text.MPart3();
    }

    private IEnumerator ZFlashOff()
    {
        yield return new WaitForSeconds(0.2f);
        WhiteBox.SetActive(false);
    }
}

