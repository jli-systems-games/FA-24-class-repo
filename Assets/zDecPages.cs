using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zDecPages : MonoBehaviour
{
    public GameObject self;
    public GameObject item;

    public void zSwitch()
    {
        self.SetActive(true);
        item.SetActive(false);
    }
}
