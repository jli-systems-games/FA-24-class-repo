using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeteEnd : MonoBehaviour
{
    public Material _e,_b;
    public TMP_Text _t;
    int target = 2;
    MeshRenderer _mr;
    void Start()
    {
        _mr = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("EndPoint"))
        {
            //switch state/ change materials;
            int outputs = other.GetComponentInParent<DrawLine>().Energy;
            if (outputs == target) _mr.material = _e;
            else if (outputs > target)
            {
                _mr.material = _b;
                _t.text = "You Blew it!";
            }
            
        }
    }
   
}
