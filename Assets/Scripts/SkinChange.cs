using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinChange : MonoBehaviour
{

    public GameObject shrimp;
    public GameObject hat;
    public GameObject goku;
    public GameObject gojo;
    public GameObject mermaid;
    public GameObject banana;

    public static bool defaultShrimp;
    public static bool hatShrimp;
    public static bool gokuShrimp;
    public static bool gojoShrimp;
    public static bool mermaidShrimp;
    public static bool bananaShrimp;


    public void DefaultShrimp()
    {
        shrimp.SetActive(true);
        hat.SetActive(false);
        goku.SetActive(false);
        gojo.SetActive(false);
        mermaid.SetActive(false);
        banana.SetActive(false);

        defaultShrimp = true;
        hatShrimp = false;
        gokuShrimp = false;
        gojoShrimp = false;
        mermaidShrimp = false;
        bananaShrimp = false;
    }


    public void HatShrimp()
    {
        shrimp.SetActive(false);
        hat.SetActive(true);
        goku.SetActive(false);
        gojo.SetActive(false);
        mermaid.SetActive(false);
        banana.SetActive(false);

        defaultShrimp = false;
        hatShrimp = true;
        gokuShrimp = false;
        gojoShrimp = false;
        mermaidShrimp = false;
        bananaShrimp = false;
    }

    public void GokuShrimp()
    {
        shrimp.SetActive(false);
        hat.SetActive(false);
        goku.SetActive(true);
        gojo.SetActive(false);
        mermaid.SetActive(false);
        banana.SetActive(false);

        defaultShrimp = false;
        hatShrimp = false;
        gokuShrimp = true;
        gojoShrimp = false;
        mermaidShrimp = false;
        bananaShrimp = false;
    }


    public void GojoShrimp()
    {
        shrimp.SetActive(false);
        hat.SetActive(false);
        goku.SetActive(false);
        gojo.SetActive(true);
        mermaid.SetActive(false);
        banana.SetActive(false);

        defaultShrimp = false;
        hatShrimp = false;
        gokuShrimp = false;
        gojoShrimp = true;
        mermaidShrimp = false;
        bananaShrimp = false;
    }


    public void MerShrimp()
    {
        shrimp.SetActive(false);
        hat.SetActive(false);
        goku.SetActive(false);
        gojo.SetActive(false);
        mermaid.SetActive(true);
        banana.SetActive(false);

        defaultShrimp = false;
        hatShrimp = false;
        gokuShrimp = false;
        gojoShrimp = false;
        mermaidShrimp = true;
        bananaShrimp = false;
    }


    public void BananaShrimp()
    {
        shrimp.SetActive(false);
        hat.SetActive(false);
        goku.SetActive(false);
        gojo.SetActive(false);
        mermaid.SetActive(false);
        banana.SetActive(true);

        defaultShrimp = false;
        hatShrimp = false;
        gokuShrimp = false;
        gojoShrimp = false;
        mermaidShrimp = false;
        bananaShrimp = true;


    }

    
}

