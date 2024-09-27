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
    public detectItem mikutarget;
    public detectItem cointarget;
    public detectItem easeltarget;
    public detectItem sofatarget;
    public detectItem artsuppliestarget;
    public detectItem gramophonetarget;
    public AudioSource audioPlayer;
    public bool snappedPic;
    public string[] objectives;
    public string[] congratulatoryMessages;
    public string randomObjective;
    public string randomMessage;
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

    private IEnumerator correctItem()
    {
        yield return new WaitForSeconds(1f);
        randomizeMessage();
        yield return new WaitForSeconds(1f);
        randomObjective = objectives[Random.Range(0, objectives.Length)];
        text.text = "NOW, TAKE A PICTURE OF " + randomObjective;
    }

    public void randomizeObjective()
    {
        randomObjective = objectives[Random.Range(0, objectives.Length)];
        text.text = "TAKE A PICTURE OF " + randomObjective;
    }

    public void randomizeMessage()
    {
        randomMessage = congratulatoryMessages[Random.Range(0, congratulatoryMessages.Length)];
        text.text = randomMessage;
    }

    void Start()
    {
        randomizeObjective();
    }

    // Update is called once per frame
    void Update()
    {
        if (zoomed.isZoomed == true && Input.GetKeyDown(KeyCode.Mouse0))
        {
            snappedPic = true;
            StartCoroutine(snapped());

            if (mikutarget.withinTarget == true || cointarget.withinTarget == true 
            || easeltarget.withinTarget == true || sofatarget.withinTarget == true
            || artsuppliestarget.withinTarget == true || gramophonetarget.withinTarget == true)
            {
                Debug.Log("Correct Item Snapped");
                StartCoroutine(correctItem());
            }

        }
        
    }
}
