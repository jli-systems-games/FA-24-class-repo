using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newscrit : MonoBehaviour
{

    public GameObject popupOne;
    public GameObject popupTwo;
    public GameObject popupThree;
    // Start is called before the first frame update
    
    // Update is called once per frame
    void Update()
    {
        if (popupOne.gameObject.activeSelf == false && popupTwo.gameObject.activeSelf == false && popupThree.gameObject.activeSelf == false)
        {
            Debug.Log("hello");


        }
    }
}
