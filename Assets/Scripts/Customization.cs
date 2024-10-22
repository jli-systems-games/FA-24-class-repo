using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.EventSystems;

public class Customization : MonoBehaviour
{
    public characterObject Assets;

    [SerializeField] Transform hatParent, faceParent;
    [SerializeField] GameObject hatHold, maskHold;

    GameObject item;
    MeshRenderer hatRenderer,mRenderer;
    MeshFilter hatFill, mFill;
    int pressedCount = -1;
    int maxCount;
    ///[SerializeField] MeshRenderer[] hatR, faceR;

    void Start()
    {
        maxCount = Assets.hats.Length;
        hatRenderer = hatHold.GetComponent<MeshRenderer>();
        hatFill = hatHold.GetComponent<MeshFilter>();
        
    }
    public void increaseSelection()
    {
        if(pressedCount >= maxCount - 1)
        {
            pressedCount = maxCount - 1;
        }
        else
        { 

            pressedCount++;
        }
        string tags = EventSystem.current.currentSelectedGameObject.tag;
        //Debug.Log(pressedCount);
        previewAccessory(tags);

    }
    public void decreaseSelection()
    {   
        if(pressedCount <= 0)
        {
            pressedCount = 0;
        }
        else
        {
             pressedCount--;
        }
       
        Debug.Log(pressedCount);
        string tags = EventSystem.current.currentSelectedGameObject.tag;
        previewAccessory(tags);
    }
    public void ConfirmSelection()
    {
        SelectAccessory();
    }
    void SelectAccessory()
    {

        item =  Instantiate(Assets.hats[pressedCount]);
       
        item.transform.parent = hatParent;
        item.transform.localPosition = hatParent.transform.localPosition; 
        //disable whatever preview objects that is on;
        hatHold.SetActive(false);
    }
    void previewAccessory(string _tag)
    {
        switch (_tag)
        {
            case "hats":
                hatFill.sharedMesh = Assets.hatMesh[pressedCount];
                break;
            case "mask":

                break;
        }
        
        StartCoroutine(replacingMaterials());
      
    }
    IEnumerator replacingMaterials()
    {   
        //determines which assetRenders to use;
        //determine which target renderer to use, replace hat renderer;
        MeshRenderer replaceMat = Instantiate(Assets.hatsR[pressedCount]);
        Material[] mats = new Material[replaceMat.materials.Length];
        int count = 0;
        while(count < replaceMat.materials.Length)
        {
             for (int i = 0; i < replaceMat.materials.Length; i++)
            {
                //Debug.Log(replaceMat.materials[i]);
                count++;
                mats[i] = replaceMat.materials[i];
                
                
            }
            hatRenderer.sharedMaterials = mats;
             yield return null;
        }
       
    }

}
