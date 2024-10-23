using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ammotext : MonoBehaviour
{
    public bool isMaxAmmo;
    public PlayerShoot ps;
    public TextMeshProUGUI text;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isMaxAmmo)
        {
            text.text = "" + ps.maxMunition[PlayerShoot.indexAmmo];
        }
        else
        {
            text.text = "" + ps.munition[PlayerShoot.indexAmmo];
        }
    }
}
