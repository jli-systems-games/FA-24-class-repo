using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.EventSystems;

public class Customization : MonoBehaviour
{
    public characterObject Assets;
    public bool done, customizing;
    [SerializeField] Transform hatParent, faceParent, Accessory;
    [SerializeField] GameObject hatHold, maskHold, _canvas;
    [SerializeField] Camera defaultCam, plyrCam;
    GameObject item;
    MeshRenderer replaceRenderer;
    MeshFilter hatFill, mFill;
    Vector3 ogHatPos, ogEyesPos;
    bool maskON;
    int pressedCount = -1;
    int maxCount;
   // string tags;
    ///[SerializeField] MeshRenderer[] hatR, faceR;
    MeshRenderer replaceMat;
    void Start()
    {
        if (customizing)
        {   
            ogHatPos = hatHold.transform.position;
        
            hatFill = hatHold.GetComponent<MeshFilter>();
            mFill = maskHold.GetComponent<MeshFilter>();

        }
        
        
    }
    public void increaseSelection()
    {
       
        string tags = EventSystem.current.currentSelectedGameObject.tag;
        switch (tags)
        {
            case "hats":
                maxCount = Assets.hats.Length;
                break;
            case "mask":
                maxCount = Assets.faceAccessories.Length;
                maskON = true;
                break;
        }
        if (pressedCount >= maxCount - 1)
        {
            pressedCount = maxCount - 1;
        }
        else
        { 

            pressedCount++;
        }
        
        //Debug.Log(pressedCount);
        previewAccessory(tags,pressedCount);

    }
    public void decreaseSelection()
    {
       

        if (pressedCount <= 0)
        {
            pressedCount = 0;
        }
        else
        {
             pressedCount--;
        }
       
        //Debug.Log(pressedCount);
        string tags = EventSystem.current.currentSelectedGameObject.tag;
        previewAccessory(tags, pressedCount);
    }
    public void ConfirmSelection()
    {
       SelectAccessory();
    }
   
    void SelectAccessory()
    {   //find all objects in the accessory parent
        if (customizing)
        {
            foreach(Transform t in Accessory)
                    {   // clone them and attach them to correct body parts so they move along the movement of the ragdoll;
                        item =  Instantiate(t.gameObject);
           
                        MeshRenderer _mesh = item.GetComponent<MeshRenderer>();
           
                        switch (t.tag)
                        {
                            case "hats":
                                if(_mesh.sharedMaterial != null)
                                {
                                  item.transform.parent = hatParent;
                                    if (maskON)
                                    {
                                        Vector3 newPos = new Vector3(hatParent.transform.localPosition.x, 1.31f, hatParent.transform.localPosition.z);
                                        item.transform.localPosition = newPos;
                                    }
                                    else
                                    {
                                        item.transform.localPosition = hatParent.transform.localPosition;
                                    }
                      

                                }
                                else
                                {
                                    Destroy(item.gameObject);
                                }
                     
                                break;
                            case "mask":

                                if (_mesh.sharedMaterial != null)
                                {
                                    item.transform.parent = faceParent;

                                    item.transform.localPosition = faceParent.transform.localPosition;
                                }
                                else
                                {
                                    Destroy(item.gameObject);
                                }
                    
                   
                                break;
                            default:
                                item.gameObject.SetActive(false);
                                break;
                        }

                    }
        }
        
        
       //disable whatever preview objects that is on;
        Accessory.gameObject.SetActive(false);
        plyrCam.enabled = true;
        defaultCam.enabled = false;
        _canvas.SetActive(false);
        done = true;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void previewAccessory(string _tag, int count)
    {
        switch (_tag)
        {
            case "hats":
                hatFill.sharedMesh = Assets.hatMesh[count];
                if (maskON)
                {
                    //move hatHolding obj up;
                    hatHold.transform.localPosition = new Vector3(0, 0.85f, hatHold.transform.localPosition.z);
                }
                break;
            case "mask":
                mFill.sharedMesh = Assets.facePlacement[count];
                break;
        }
        
        StartCoroutine(replacingMaterials(_tag,count ));
      
    }
    IEnumerator replacingMaterials(string cases, int _count)
    {
        
        //determines which assetRenders to use;
        switch (cases)
        {
            case "hats":
                replaceMat = Assets.hatsR[_count];
                replaceRenderer = hatHold.GetComponent<MeshRenderer>();
                break;
            case "mask":
                replaceMat = Assets.faceR[_count];
                replaceRenderer = maskHold.GetComponent<MeshRenderer>();
                break;
        }
        //determine which target renderer to use, replace hat renderer;
        if(replaceMat != null)
        {
           Material[] mats = new Material[replaceMat.sharedMaterials.Length];
            int count = 0;
            while(count < replaceMat.sharedMaterials.Length)
            {
                for (int i = 0; i < replaceMat.sharedMaterials.Length; i++)
                {
                    //Debug.Log(replaceMat.materials[i]);
                    count++;
                    mats[i] = replaceMat.sharedMaterials[i];
                
                
                }
                replaceRenderer.sharedMaterials = mats;
                yield return null;
            }
        }
     
       
    }

}
