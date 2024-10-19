using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dispense : MonoBehaviour
{
    public GameObject ice;
    public GameObject salt;


    // Start is called before the first frame update
    void Start()
    {
        Vector3 iceOrginalPosition = ice.gameObject.transform.position;
        Vector3 saltOrginalPosition = salt.gameObject.transform.position;

    }

    public void dispenseIce()
    {
        ice.gameObject.SetActive(true);
    }

    public void dispenseSalt()
    {
        salt.gameObject.SetActive(false);
    }


    void Update()
    {
        
        //ice.gameObject.transform.position = orginalPosition;
    }
}
