using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdButton : MonoBehaviour
{

    private AdManager bScript;

    public void Start()
    {
        bScript = GameObject.FindGameObjectWithTag("Ads").GetComponent<AdManager>();
    }

    public void Close()
    {
        bScript.Score++;
        this.gameObject.SetActive(false);

       

    }
    



}
