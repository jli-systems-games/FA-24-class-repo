using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StampingParent : MonoBehaviour
{ int index;
    // Start is called before the first frame update
    void Start()
    {
        index = transform.GetSiblingIndex();
    }

    // Update is called once per frame
    void Update()
    {
        
        Debug.Log("the hireachy place is " + index);
    }
}
