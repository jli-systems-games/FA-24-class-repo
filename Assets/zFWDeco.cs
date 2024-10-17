using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zFWDeco : MonoBehaviour
{
    public List<GameObject> allClothes = new();
    public List<GameObject> activeClothes = new();


    //first scene ver
    public GameObject sEyes, sGlasses, sCone, sParty, sFrosting, sBow, sStar, sSlay, iEyes, iGlasses, iCone, iParty, iFrosting, iBow, iStar, iSlay, mEyes, mGlasses, mCone, mParty, mFrosting, mBow, mStar, mSlay;

    //next scene ver
    public GameObject csEyes, csGlasses, csCone, csParty, csFrosting, csBow, csStar, csSlay, ciEyes, ciGlasses, ciCone, ciParty, ciFrosting, ciBow, ciStar, ciSlay, cmEyes, cmGlasses, cmCone, cmParty, cmFrosting, cmBow, cmStar, cmSlay;


    //first scene bools
    static public bool bsEyes, bsGlasses, bsCone, bsParty, bsFrosting, bsBow, bsStar, bsSlay, biEyes, biGlasses, biCone, biParty, biFrosting, biBow, biStar, biSlay, bmEyes, bmGlasses, bmCone, bmParty, bmFrosting, bmBow, bmStar, bmSlay;


    public void Start()
    {
        Debug.Log("2" + bsEyes);

        if (bsEyes == true)
        {
            csEyes.SetActive(true);
        }
        if (bsGlasses == true)
        {
            csGlasses.SetActive(true);
        }
        if (bsCone == true)
        {
            csCone.SetActive(true);
        }
        if (bsParty == true)
        {
            csParty.SetActive(true);
        }
        if (bsFrosting == true)
        {
            csFrosting.SetActive(true);
        }
        if (bsBow == true)
        {
            csBow.SetActive(true);
        }
        if (bsStar == true)
        {
            csStar.SetActive(true);
        }
        if (bsSlay == true)
        {
            csSlay.SetActive(true);
        }
        if (biEyes == true)
        {
            ciEyes.SetActive(true);
        }
        if (biGlasses == true)
        {
            ciGlasses.SetActive(true);
        }
        if (biCone == true)
        {
            ciCone.SetActive(true);
        }
        if (biParty == true)
        {
            ciParty.SetActive(true);
        }
        if (biFrosting == true)
        {
            ciFrosting.SetActive(true);
        }
        if (biBow == true)
        {
            ciBow.SetActive(true);
        }
        if (biStar == true)
        {
            ciStar.SetActive(true);
        }
        if (biSlay == true)
        {
            ciSlay.SetActive(true);
        }
        if (biSlay == true)
        {
            ciSlay.SetActive(true);
        }
        if (bmEyes == true)
        {
            cmEyes.SetActive(true);
        }
        if (bmGlasses == true)
        {
            cmGlasses.SetActive(true);
        }
        if (bmCone == true)
        {
            cmCone.SetActive(true);
        }
        if (bmParty == true)
        {
            cmParty.SetActive(true);
        }
        if (bmFrosting == true)
        {
            cmFrosting.SetActive(true);
        }
        if (bmBow == true)
        {
            cmBow.SetActive(true);
        }
        if (bmStar == true)
        {
            cmStar.SetActive(true);
        }
        if (bmSlay == true)
        {
            cmSlay.SetActive(true);
        }
        if (bmSlay == true)
        {
            cmSlay.SetActive(true);
        }
    }

    public void zAllClothesList()
    {
        activeClothes.Clear();

        foreach (GameObject item in allClothes)
        {
            if (item.activeSelf)
            {
                activeClothes.Add(item);
            }
        }
    }

    public void zActiveClothesList()
    {
        foreach (GameObject item in activeClothes)
        {
            item.SetActive(true);
        }
    }

    public void zKeepBools()
    {
        if (sEyes.activeSelf)
        {
            bsEyes = true;
        }
        if (sGlasses.activeSelf)
        {
            bsGlasses = true;
        }
        if (sCone.activeSelf)
        {
            bsCone = true;
        }
        if (sParty.activeSelf)
        {
            bsParty = true;
        }
        if (sFrosting.activeSelf)
        {
            bsFrosting = true;
        }
        if (sBow.activeSelf)
        {
            bsBow = true;
        }
        if (sStar.activeSelf)
        {
            bsStar = true;
        }
        if (sSlay.activeSelf)
        {
            bsSlay = true;
        }
        if (sSlay.activeSelf)
        {
            bsSlay = true;
        }
        if (iEyes.activeSelf)
        {
            biEyes = true;
        }
        if (iGlasses.activeSelf)
        {
            biGlasses = true;
        }
        if (iCone.activeSelf)
        {
            biCone = true;
        }
        if (iParty.activeSelf)
        {
            biParty = true;
        }
        if (iFrosting.activeSelf)
        {
            biFrosting = true;
        }
        if (iBow.activeSelf)
        {
            biBow = true;
        }
        if (iStar.activeSelf)
        {
            biStar = true;
        }
        if (iSlay.activeSelf)
        {
            biSlay = true;
        }
        if (mEyes.activeSelf)
        {
            bmEyes = true;
        }
        if (mGlasses.activeSelf)
        {
            bmGlasses = true;
        }
        if (mCone.activeSelf)
        {
            bmCone = true;
        }
        if (mParty.activeSelf)
        {
            bmParty = true;
        }
        if (mFrosting.activeSelf)
        {
            bmFrosting = true;
        }
        if (mBow.activeSelf)
        {
            bmBow = true;
        }
        if (mStar.activeSelf)
        {
            bmStar = true;
        }
        if (mSlay.activeSelf)
        {
            bmSlay = true;
        }

        Debug.Log(bsEyes);
    }

    public void zPutOnClothes()
    {
        if (bsEyes == true)
        {
            csEyes.SetActive(true);
        }
        if (bsGlasses == true)
        {
            csGlasses.SetActive(true);
        }
        if (bsCone == true)
        {
            csCone.SetActive(true);
        }
        if (bsParty == true)
        {
            csParty.SetActive(true);
        }
        if (bsFrosting == true)
        {
            csFrosting.SetActive(true);
        }
        if (bsBow == true)
        {
            csBow.SetActive(true);
        }
        if (bsStar == true)
        {
            csStar.SetActive(true);
        }
        if (bsSlay == true)
        {
            csSlay.SetActive(true);
        }
        if (biEyes == true)
        {
            ciEyes.SetActive(true);
        }
        if (biGlasses == true)
        {
            ciGlasses.SetActive(true);
        }
        if (biCone == true)
        {
            ciCone.SetActive(true);
        }
        if (biParty == true)
        {
            ciParty.SetActive(true);
        }
        if (biFrosting == true)
        {
            ciFrosting.SetActive(true);
        }
        if (biBow == true)
        {
            ciBow.SetActive(true);
        }
        if (biStar == true)
        {
            ciStar.SetActive(true);
        }
        if (biSlay == true)
        {
            ciSlay.SetActive(true);
        }
        if (biSlay == true)
        {
            ciSlay.SetActive(true);
        }
        if (bmEyes == true)
        {
            cmEyes.SetActive(true);
        }
        if (bmGlasses == true)
        {
            cmGlasses.SetActive(true);
        }
        if (bmCone == true)
        {
            cmCone.SetActive(true);
        }
        if (bmParty == true)
        {
            cmParty.SetActive(true);
        }
        if (bmFrosting == true)
        {
            cmFrosting.SetActive(true);
        }
        if (bmBow == true)
        {
            cmBow.SetActive(true);
        }
        if (bmStar == true)
        {
            cmStar.SetActive(true);
        }
        if (bmSlay == true)
        {
            cmSlay.SetActive(true);
        }
        if (bmSlay == true)
        {
            cmSlay.SetActive(true);
        }
    }
}
