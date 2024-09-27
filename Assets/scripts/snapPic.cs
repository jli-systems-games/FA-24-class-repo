using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class snapPic : MonoBehaviour
{

    public GameObject snap;
    public FirstPersonController zoomed;
    public detectItem cubetarget;
    public detectItem cointarget;
    public AudioSource audioPlayer;
    public bool snappedPic;
    public string[] objectives;
    public string randomObjective;
    public TextMeshProUGUI text;

    private IEnumerator snapped()
    {
        audioPlayer.Play();
        snap.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        snap.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        snappedPic = false;
    }

    public void randomizeObjective()
    {
        randomObjective = objectives[Random.Range(0, objectives.Length)];
    }

    void Start()
    {
        randomizeObjective();
        text.text = "Take a picture of a " + randomObjective;
    }

    // Update is called once per frame
    void Update()
    {
        if (zoomed.isZoomed == true && Input.GetKeyDown(KeyCode.Mouse0))
        {
            snappedPic = true;
            StartCoroutine(snapped());

            if (cubetarget.withinTarget == true || cointarget.withinTarget == true)
            {
                Debug.Log("Correct Item Snapped");
            }

        }
        
    }
}
