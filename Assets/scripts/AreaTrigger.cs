using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AreaTrigger : MonoBehaviour
{

    private float enterTimer = 0f;
    public GameObject HoldE;
    public GameObject eTimer;
    public Image timer;
    public float EHoldTime = 2f;
    public GameObject AreaObject;
    public GameObject triggerObject;
    public GameObject OpenSign;


    private bool canHoldToOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        AreaObject.SetActive(false);
        eTimer.SetActive(false);
        HoldE.SetActive(false);
        OpenSign.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        if (canHoldToOpen)
        {
            HoldToOpen();
        }
    }

  /*  private IEnumerator EnableHoldToOpenAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        canHoldToOpen = true;
    }*/

    private void HoldToOpen()
    {
        if (Input.GetKey(KeyCode.E))
        {
            HoldE.GetComponent<TextMeshProUGUI>().alpha = 0f;
            eTimer.SetActive(true);
           // audioSource.SetActive(true);
            enterTimer += Time.deltaTime;

            float progress = enterTimer / EHoldTime;
            timer.GetComponent<Image>().fillAmount = Mathf.Clamp01(progress);
           // AreaObject.SetActive(true);

            if (enterTimer >= EHoldTime)
            {
                // SceneManager.LoadScene("Spark");
                AreaObject.SetActive(true);
                triggerObject.SetActive(false);
                OpenSign.SetActive(true);
                enterTimer = 0f;
            }
        }
        else
        {
            
            enterTimer = 0f;
            eTimer.SetActive(false);
            //audioSource.SetActive(false);
            HoldE.GetComponent<TextMeshProUGUI>().alpha = 1f;
            timer.GetComponent<Image>().fillAmount = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {         
            HoldE.SetActive(true);
            canHoldToOpen = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {         
            HoldE.SetActive(false);
            canHoldToOpen = false;
        }
    }
}
