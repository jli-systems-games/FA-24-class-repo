using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Outfit_Chosen : MonoBehaviour
{
    public GameObject outfit;
    public GameObject fallBg;
    public GameObject greyBackground;
    public GameObject inventory;
    public GameObject resetButton;

    private Transform initialPos;


    public GameObject[] outfitPieces;

    //public List<GameObject> preset1;
    //public bool preset1Full;

    //public List<GameObject> preset2;
    //public bool preset2Full;

    //public List<GameObject> preset3;
    //public bool preset3Full;

    //public List<GameObject> preset4;
    //public bool preset4Full;

    //public TextMeshProUGUI textMeshProUGUI;

    public void Start()
    {
        initialPos = outfit.GetComponent<Transform>();
    }

    public void outfitDone()
    {
        outfit.GetComponent<Transform>().position = Vector3.zero;
        fallBg.SetActive(true);
        greyBackground.SetActive(false);
        inventory.SetActive(false);
        resetButton.SetActive(true);
    }

    public void Reset()
    {
        outfit.GetComponent<Transform>().position = new Vector3(4.35f,0,0);
        inventory.SetActive(true);
        resetButton.SetActive(false);

        fallBg?.SetActive(false);
        greyBackground.SetActive(true);

        for(int i = 0; i < outfitPieces.Length; i++)
        {
            outfitPieces[i].SetActive(false);
        }
    }

    //public void saveOutfit()
    //{
    //    for (int i = 0; i < outfitPieces.Length; i++)
    //    {
    //        if (outfitPieces[i].activeInHierarchy)
    //        {
    //            if (!preset1Full)
    //            {

    //            }
    //            else if (!preset2Full)
    //            {

    //            }
    //            else if (!preset3Full)
    //            {

    //            }
    //            else if (!preset4Full)
    //            {

    //            }
    //            else
    //            {
    //                textMeshProUGUI.enabled = true;
    //            }
    //        }
    //    }
    //}

    public void clearPreset(List<GameObject> preset)
    {
        preset.Clear();

    }
}
