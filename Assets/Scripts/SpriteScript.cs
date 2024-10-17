using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteScript : MonoBehaviour
{
    public GameObject shrimp;
    public GameObject hat;
    public GameObject goku;
    public GameObject gojo;
    public GameObject mermaid;
    public GameObject banana;
    // Start is called before the first frame update
    void Start()
    {
        if(SkinChange.defaultShrimp == true)
        {
            shrimp.SetActive(true);
        }

        if (SkinChange.hatShrimp == true)
        {
            hat.SetActive(true);
        }


        if (SkinChange.gokuShrimp == true)
        {
            goku.SetActive(true);
        }


        if (SkinChange.gojoShrimp == true)
        {
            gojo.SetActive(true);
        }


        if (SkinChange.mermaidShrimp == true)
        {
            mermaid.SetActive(true);
        }


        if (SkinChange.bananaShrimp == true)
        {
            banana.SetActive(true);
        }
    }



}
