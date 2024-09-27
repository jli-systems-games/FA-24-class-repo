using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snapPic : MonoBehaviour
{

    public GameObject snap;
    public FirstPersonController zoomed;
    public detectItem target;
    public AudioSource audioPlayer;

    private IEnumerator snapped()
    {
        audioPlayer.Play();
        snap.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        snap.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (zoomed.isZoomed == true && Input.GetKeyDown(KeyCode.Mouse0))
        {
            
            StartCoroutine(snapped());

            if (target.withinTarget == true)
            {
                Debug.Log("got item");
            }

        }
        
    }
}
